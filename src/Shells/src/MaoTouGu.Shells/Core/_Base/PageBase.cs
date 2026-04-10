using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells
{
    public abstract class PageBase : ViewModelBase
    {
        private bool _removable;

        protected PageBase()
        {
            Id = Interlocked.Increment(ref _Global_VID);
        }

        /// <summary>
        /// 关于退出的行为。
        /// </summary>
        /// <returns></returns>
        #region Close

        protected void Close() => Stop();

        protected sealed override void StopAfter()
        {
            var appModel = Ioc.SafeGet<IAppModel>();
            
            appModel.UnsetViewCache(this);
        }

        #endregion

        protected void ShowFlyout()
        {
            var appModel = Ioc.Get<IAppModel>();
            var service  = appModel.GetFlyoutService(this);

            service?.Flyout();
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
        /// 
        /// </summary>
        public string InstanceID { get; protected set; }
        
        public bool Singleton { get; protected set; }


        public bool Removable
        {
            get => _removable;
            protected set => SetValue(ref _removable, value);
        }
    }
}