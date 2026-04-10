using System.Runtime.CompilerServices;
using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells
{
    public abstract class DialogBase : ViewModelBase
    {
        private string _noButtonText;
        private string _okButtonText;
        
        protected DialogBase()
        {
            Id = Interlocked.Increment(ref _Global_DID);
        }

        /// <summary>
        /// 请求进入繁忙状态。
        /// </summary>
        /// <returns>返回一个繁忙状态管理器。</returns>
        public sealed override IBusyStateManager AcquireBusyState()
        {
            var appModel  = Ioc.Get<IAppModel>();
            var recipient = appModel.GetBusyStateRecipient(this);

            if (recipient is null)
            {
                return null;
            }

            return new BusyStateManager(recipient);
        }
        /// <summary>
        /// 关于退出的行为。
        /// </summary>
        /// <returns></returns>
        #region Close

        protected sealed override void StopAfter()
        {
            var srv = Ioc.SafeGet<IAppModel>();


            //
            // 确保关闭此选项。
            CloseHandler?.Invoke(this);
            //
            // 结束当前的Session。
            //srv.UnsetViewCache(this);
            srv.UnsetViewCache(this);
        }

        #endregion

        /// <summary>
        /// 判断当前对话框是否可以完成退出操作（完成退出时可能会返回结果，取决于派生类如何重写逻辑）。
        /// </summary>
        /// <returns>如果为true，则表示可以完成退出操作，否则返回false。</returns>
        protected virtual bool CanFinish() => true;

        protected virtual void Finish()
        {

        }

        /// <summary>
        /// 设置属性值，并提示数据更新
        /// </summary>
        /// <param name="source">原始的字段</param>
        /// <param name="value">值字段</param>
        /// <param name="name">属性名</param>
        /// <typeparam name="E">类型数据</typeparam>
        /// <returns>返回是否更新</returns>
        protected bool TryFinishAndSetValue<E>(ref E source, E value, [CallerMemberName] string name = "")
        {
            if (string.IsNullOrEmpty(name)) return false;
            source = value;
            RaiseUpdated(name);
            TryFinish();

            return true;
        }

        protected virtual void OnCancel()
        {
        }

        protected void TryFinish() => CompleteCommand?.RaiseUpdate();

        public void Cancel()
        {
            //
            // 如果这个对话框没有完成Close流程，那么执行此次操作即可完成Close流程。
            CloseHandler?.Invoke(this);

            //
            //
            OnCancel();
        }

        public void Complete()
        {
            if (!CanFinish())
            {
                return;
            }

            //
            // 如果这个对话框没有完成Close流程，那么执行此次操作即可完成Close流程。
            //
            // 2025/4/23 1:47 发现一个BUG，对话框如果不提前调用CloseHandler方法，
            // 一旦TCS被SetResult，异步方法就会让对话框的顺序失效。
            //
            // 所有操作都得后置。
            CloseHandler?.Invoke(this);

            Finish();

        }



        public string OkButtonText
        {
            get => _okButtonText;
            set => SetValue(ref _okButtonText, value);
        }
        
        public string NoButtonText
        {
            get => _noButtonText;
            set => SetValue(ref _noButtonText, value);
        }

        protected internal PageBase Owner { get; internal set; }

        protected internal Action<DialogBase> CloseHandler { get; set; }

        public ICommandEX CompleteCommand { get; protected init; }
        public ICommandEX CancelCommand   { get; protected init; }
    }
}