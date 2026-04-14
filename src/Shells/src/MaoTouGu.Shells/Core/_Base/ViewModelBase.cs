using MaoTouGu.Shells.Core;
using MaoTouGu.Foundation;
using MaoTouGu.Foundation.Core;
using NLog.Fluent;

namespace MaoTouGu.Shells
{
    public abstract class ViewModelBase : LifetimeExtended
    {
        internal static volatile int _Global_VID;
        internal static volatile int _Global_DID;

        static ViewModelBase()
        {
            _Global_DID = 0x12;
            _Global_VID = 0x12;
        }

        protected ViewModelBase()
        {
            Logger = LoggerExt.GetLogger(this);
            Title  = I18N.GetViewModel(this);
        }


        //------------------------------------------------------------
        //
        //                 OnException / OnLogging
        //
        //------------------------------------------------------------
        //
        // 用于实现基类调用出错时，输出调试信息的帮助方法。
        #region Logging Helper Methods

        protected sealed override void OnException(string methodName, Exception ex)
        {
            Logger.Warn($"{GetType().Name}实例在调用方法:{methodName}时遇到错误，错误信息如下:{ex.Message}");
        }

        protected sealed override void OnLogging(string message)
        {
            Logger.Warn($"{GetType().Name}实例在调用方法时遇到错误，错误信息如下:{message}");
        }

        #endregion


        //------------------------------------------------------------
        //
        //                       Navigate
        //
        //------------------------------------------------------------

        #region Navigate

        public Task<bool> Receive(object[] args) => OnReceive(args);

        public Task<bool> Navigate<T>() where T : PageBase => Navigate<T>(null);
        
        public Task<bool> Navigate<T>(params object[] args) where T : PageBase => Navigate(ClassStatic.CreateInstance<T>(), args);

        public Task<bool> Navigate(PageBase target) => Navigate(target, null);

        public Task<bool> Navigate(PageBase target, params object[] args)
        {
            return Ioc.SafeGet<IAppModel>()?.Navigate(target, args);
        }

        #endregion


        protected virtual Task<bool> OnReceive(object[] args) => Task.FromResult(true);
        
        //------------------------------------------------------------
        //
        //                       GetView
        //
        //------------------------------------------------------------
        //
        // 用于实现低耦合的方式与V层通讯。

        /// <summary>
        /// 获得View。
        /// </summary>
        /// <typeparam name="T">指定视图的类型。</typeparam>
        /// <returns>返回视图实例，可能为空。</returns>
        protected T GetView<T>() where T : class
        {
            var viewCache = Ioc.Get<IAppModel>()
                               .GetViewCache(this);

            return viewCache as T;
        }

        /// <summary>
        /// 请求进入繁忙状态。
        /// </summary>
        /// <returns>返回一个繁忙状态管理器。</returns>
        public abstract IBusyStateManager AcquireBusyState();

        private bool   _isChange;
        private string _title;
        
        
        /// <summary>
        /// 设置当前的数据状态。
        /// </summary>
        /// <param name="state">数据状态。</param>
        public void SetDirtyState(bool state) => IsChange = state;
        
        /// <summary>
        /// 包装过的标题。
        /// </summary>
        public string FriendlyTitle => IsChange ? $"● {Title}" : Title;
        
        public bool IsChange
        {
            get => _isChange;
            protected set => SetValue(ref _isChange, value);
        }
        
        public string Title
        {
            get => _title;
            protected set
            {
                SetValue(ref _title, value);
                RaiseUpdated(nameof(FriendlyTitle));
            }
        }
        
        /// <summary>
        /// 日志记录
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// 唯一标识符
        /// </summary>
        public int Id { get; protected private set; }
    }
}