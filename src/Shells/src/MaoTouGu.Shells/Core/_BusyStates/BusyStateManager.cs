using System.Diagnostics;
using MaoTouGu.Shells.Core;
using OperationQueue = System.Collections.Generic.Queue<MaoTouGu.Shells.Core.ObservableAsyncOperation>;


namespace MaoTouGu.Shells.Core
{
    /// <summary>
    /// <see cref="BusyStateManager"/> 类型用于维护所有窗体的繁忙状态管理，为开发者提供BusyState的启用和停止、长耗时任务的友好视觉效果以及超长任务的终结。
    /// </summary>
    public sealed class BusyStateManager : IBusyStateManager
    {
        private const int Timespan_Millisecond = 50;

        private readonly OperationQueue      _Operations;
        private readonly IBusyStateRecipient _Recipient;

        private int  _CycleTime;
        private int  _TotalCount;
        private bool _IsIndeterminate;

        private IDispatcherTimer         _Timer;
        private ObservableAsyncOperation _Operation;

        //------------------------------------------------------------
        //
        //                  Constructors
        //
        //------------------------------------------------------------
        public BusyStateManager(IBusyStateRecipient recipient)
        {
            _Operations = new OperationQueue();
            _Recipient  = recipient ?? throw new ArgumentNullException(nameof(recipient));
        }
        

        //------------------------------------------------------------
        //
        //                      GetDocumentLayout
        //
        //------------------------------------------------------------

        private void StartWork()
        {
            if (_Operations.Count <= 0)
            {
                return;
            }


            _Timer = _Recipient.GetTimer(Timespan_Millisecond, DoPool);
            _Timer.Start();
        }

        async void InternalExecute(ObservableAsyncOperation operation)
        {
            await operation.Run();


            if (operation is ConditionalAsyncOperation cao && !cao.CanNext)
            {
                cao.Callback?.Invoke();
                Stop();
            }
        }

        private void DoPool()
        {
            //
            // 判断当前的计时器是否需要进行工作。
            if (_Operations.Count == 0)
            {
                if (_CycleTime == 0)
                {
                    Stop();
                }
                else
                {
                    _Recipient.ReportProgress(_TotalCount + 1);
                    _CycleTime = 0;
                }

                return;
            }

            try
            {

                if (_Operation is null)
                {
                    //
                    // 如果Operation为空或者Operation执行完毕
                    _Operation = _Operations.Dequeue();

                    //
                    //
                    _Recipient.SetBusyText(_Operation.Text);
                    InternalExecute(_Operation);
                    return;
                }
                
                if (_Operation.IsCompleted)
                {
                    //
                    // 解除长时间等待的状态。
                    if (_Recipient.IsDeterminateState())
                    {
                        _Recipient.ChangeToDeterminateState();
                    }

                    //
                    //
                    _Operation = _Operations.Dequeue();
                    _TotalCount++;

                    //
                    // 如果Operation执行完毕，则报告
                    _Recipient.ReportProgress(_TotalCount);
                    _Recipient.SetBusyText(_Operation.Text);
                    InternalExecute(_Operation);
                    return;
                }
                
                
                _CycleTime++;

                //
                // 等待当前任务的执行。
                if (_CycleTime > 100)
                {
                    if (_CycleTime > 200)
                    {
                        //
                        // 如果执行时间超过60s，即1200
                        // 强制结束所有任务。
                        _Recipient.ShouldLongTimeTaskShutdown();
                        Stop();
                        return;
                    }

                    //
                    // 如果执行时间超过5s，即1000
                    if (!_IsIndeterminate)
                    {
                        _Recipient.ChangeToIndeterminateState();
                        _IsIndeterminate = true;
                    }
                }

                if (_Operation is not null && _Operation.IsBusyTextChanged)
                {
                    _Recipient.SetBusyText(_Operation.Text);
                    _Operation.FinishBusyTextChanged();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Execute()
        {
            _Recipient.ReportProgress(0);
            _Recipient.ReportOperationCount(_Operations.Count);
            _Recipient.Enter();
            StartWork();
        }

        public void Stop()
        {
            _Timer.Stop();
            _Recipient.ReportProgress(0);
            _Recipient.ReportOperationCount(0);
            _Recipient.ChangeToDeterminateState();
            _Recipient.Leave();

        }

        //------------------------------------------------------------
        //
        //              IBusyStateManager Interfaces
        //
        //------------------------------------------------------------
        public IBusyStateManager Execute(Action operation) => Execute(string.Empty, operation);
        public IBusyStateManager Condition(Func<bool> operation) => Condition(string.Empty, operation);
        public IBusyStateManager Condition(Func<bool> expression, Action callback) => Condition(string.Empty, expression, callback);
        public IBusyStateManager Execute(Action<List<object>> operation, List<object> parameters) => Execute(string.Empty, operation, parameters);

        public IBusyStateManager Execute(string text, Action operation)
        {
            _Operations.Enqueue(new ExecuteAsyncOperation
            {
                Text       = text,
                Expression = operation,
            });

            return this;
        }

        public IBusyStateManager Condition(string text, Func<bool> operation)
        {
            _Operations.Enqueue(new ConditionalAsyncOperation
            {
                Text       = text,
                Expression = operation,
            });
            return this;
        }

        public IBusyStateManager Condition(string text, Func<bool> operation, Action callback)
        {
            _Operations.Enqueue(new ConditionalAsyncOperation
            {
                Text       = text,
                Expression = operation,
                Callback   = callback,
            });
            return this;
        }

        public IBusyStateManager Execute(ObservableAsyncOperation operation)
        {
            if (operation is null)
            {
                return this;
            }

            _Operations.Enqueue(operation);
            return this;
        }

        public IBusyStateManager Execute(string text, Action<List<object>> operation, List<object> parameters)
        {
            _Operations.Enqueue(new ParameterAsyncOperation
            {
                Text       = text,
                Expression = operation,
                Parameters = parameters,
            });
            return this;
        }
    }
}