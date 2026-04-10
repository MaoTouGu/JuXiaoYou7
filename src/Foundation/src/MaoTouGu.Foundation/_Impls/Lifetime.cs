using System.Diagnostics;

namespace MaoTouGu.Foundation
{
    public abstract class Lifetime : ObservableObject, ICancelable, ILifetime, INotifyPropertyChangedEX
    {

        void INotifyPropertyChangedEX.RaiseUpdated(string name)
        {
            RaiseUpdated(name);
        }

        /// <summary>
        /// 用于实现基类调用出错时，输出调试信息的帮助方法。
        /// </summary>
        /// <param name="message">要输出的消息。</param>
        [DebuggerHidden]
        protected virtual void OnLogging(string message)
        {
            
        }

        /// <summary>
        /// 用于实现基类调用出错时，输出调试信息的帮助方法。
        /// </summary>
        /// <param name="methodName">调用的方法名。</param>
        /// <param name="ex">异常信息。</param>
        [DebuggerHidden]
        protected virtual void OnException(string methodName, Exception ex)
        {
            
        }
        
        //-------------------------------------------------------------
        //
        //          Start
        //
        //-------------------------------------------------------------


        #region Start

        /// <summary>
        /// 启动一个视图模型，注意：视图模型只能启动一次。
        /// </summary>
        public void Start()
        {
            if (IsInitialized)
            {
                OnLogging("Start() was called, but this instance is initialized");
                return;
            }


            try
            {
                StartBefore();
                OnStart();
                StartAfter();
            }
            catch(Exception e)
            {
                OnException(nameof(Start), e);
            }
            IsInitialized = true;
            IsRunning     = true;
        }

        /// <summary>
        /// 启动一个视图模型
        /// </summary>
        protected virtual void StartBefore()
        {
        }

        /// <summary>
        /// 启动一个视图模型
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// 启动一个视图模型
        /// </summary>
        protected virtual void StartAfter()
        {
        }

        #endregion



        //-------------------------------------------------------------
        //
        //          Stop
        //
        //-------------------------------------------------------------


        #region Stop

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            //
            //
            if (IsRunning && !IsInitialized)
            {
                OnLogging("Stop() was called, but this instance is not initialized");
                return;
            }
            

            try
            {
                
                StopBefore();
                OnStop();
                StopAfter();
                Dispose();
            }
            catch(Exception e)
            {
                OnException(nameof(Stop), e);
            }
            
            IsRunning     = false;
            IsInitialized = false;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void StopBefore()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnStop()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void StopAfter()
        {
        }

        #endregion


        //-------------------------------------------------------------
        //
        //          ICancelable
        //
        //-------------------------------------------------------------

        #region ICancelable

        protected virtual void ReleaseUnmanagedResources()
        {
        }

        protected virtual void ReleaseManagedResources()
        {
        }

        protected void Dispose(bool disposing)
        {
            ReleaseManagedResources();

            if (disposing)
            {
                ReleaseUnmanagedResources();
                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


        /// <summary>
        /// 是否已经初始化了
        /// </summary>
        public bool IsInitialized 
        { 
            get ;
            private set;
        }

        /// <summary>
        /// 是否正在运行。
        /// </summary>
        public bool IsRunning { get; internal set; }

        /// <summary>
        /// 是否已经释放。
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 是否已经暂停运行。
        /// </summary>
        public bool IsSuspend => IsInitialized && !IsRunning;

        /// <summary>
        /// 是否已经停止。
        /// </summary>
        public bool IsStop => IsInitialized && IsDisposed;

        ~Lifetime()
        {
            Dispose(false);
        }
    }
}