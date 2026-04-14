// ----------------------------------------------------------
//            文件：InstancePage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 09:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Services;
using MaoTouGu.Shells.Behaviors;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public abstract class InstancePage : JuXiaoYouPage
    {

        private string   _occupiedUser;
        private string   _occupiedUserName;
        private bool     _isOwned;
        private bool     _isOccupied;
        private DateTime _modified;

        protected InstancePage() : base(true, false)
        {

        }

        internal void FindUserName()
        {
            if (string.IsNullOrEmpty(OccupiedUser))
            {
                OccupiedUserName = null;
            }
            else
            {

                OccupiedUserName = Ioc.Get<IUserService>()
                                      .Dictionary
                                      .GetValueOrDefault(OccupiedUser)
                                     ?.DisplayName;
            }
        }

        protected sealed override async void StartAfter()
        {
            var api = Ioc.SafeGet<IResourceLockApiContract>();

            if (api is null)
            {
                return;
            }

            //
            // 必须在OnStart时初始化InstanceID
            if (string.IsNullOrEmpty(InstanceID))
            {
                return;
            }

            var r = await api.HasLockAsync(InstanceID);

            //
            // 如果占用
            if (!r.IsFinished)
            {
                return;
            }

            //
            // 
            if (Guid.TryParse(r.Reason, out _) && r.Reason != GlobalSettings.UserID)
            {
                //
                //
                IsOccupied   = true;
                IsOwned      = false;
                OccupiedUser = r.Value;
                FindUserName();
                NotifyThisDocumentLocked();
                return;
            }

            try
            {
                //
                // 尝试获得锁。
                var result = await api.GetLockAsync(InstanceID);

                //
                // 尝试获得锁失败，释放。
                if (!result.IsFinished)
                {
                    await api.ReleaseLockAsync(InstanceID);
                }


                IsOccupied   = false;
                IsOwned      = true;
                OccupiedUser = Ioc.SafeGet<IWebApi>()?.UserID;
            }
            catch(Exception e)
            {
                IsOccupied       = false;
                IsOwned          = false;
                OccupiedUser     = null;
                OccupiedUserName = null;
                OnException(nameof(StartAfter), e);
            }
            finally
            {
                FindUserName();
            }

            NotifyThisDocumentLocked();
        }

        void NotifyThisDocumentLocked()
        {
            if (IsOwned)
            {
                return;
            }

            var view   = GetView<ForestPage>();
            var window = Xaml.FindVisualParent<MTGWindow>(view);

            // Xaml.FindVisualChildren<FrameworkElement, Button, TextBox>(view, 32)
            //     .ForEach(x => x.IsEnabled = false);

            WindowBehavior.FlyoutObject(window, new OccupiedNotifyPanel());
        }

        protected sealed override async void StopBefore()
        {
            var api = Ioc.SafeGet<IResourceLockApiContract>();

            if (api is null)
            {
                return;
            }

            //
            // 必须在OnStart时初始化InstanceID
            if (string.IsNullOrEmpty(InstanceID))
            {
                return;
            }


            var r = await api.HasLockAsync(InstanceID);

            if (!r.IsFinished)
            {
                return;
            }

            //
            // 释放锁。

            try
            {
                await api.ReleaseLockAsync(InstanceID);
                Debug.WriteLine($"Release Lock -> {InstanceID}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        /// <summary>
        /// 刷新占用状态。
        /// </summary>
        protected internal void RefreshOccupiedState()
        {

        }

        protected bool CanModified() => IsOwned;

        protected bool CanModified<T>(T target) => IsOwned && target is not null;

        public DateTime Modified
        {
            get => _modified;
            set => SetValue(ref _modified, value);
        }
        public bool IsOccupied
        {
            get => _isOccupied;
            set => SetValue(ref _isOccupied, value);
        }

        public bool IsOwned
        {
            get => _isOwned;
            set => SetValue(ref _isOwned, value);
        }

        public string OccupiedUserName
        {
            get => _occupiedUserName;
            set => SetValue(ref _occupiedUserName, value);
        }

        public string OccupiedUser
        {
            get => _occupiedUser;
            set => SetValue(ref _occupiedUser, value);
        }


    }
}