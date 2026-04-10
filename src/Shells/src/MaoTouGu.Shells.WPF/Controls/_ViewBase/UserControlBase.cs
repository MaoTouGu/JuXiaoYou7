using MaoTouGu.Shells.Behaviors;
using MaoTouGu.Shells.Languages;
using Timer = System.Threading.Timer;

namespace MaoTouGu.Shells.Controls
{
    public abstract class UserControlBase : UserControl, IFlyoutElementRecipient, IFlyoutService
    {
        protected readonly List<FrameworkElement> FlyoutElements;

        private bool  _init;
        private int   _lastCount;
        private int   _time;
        private Timer _monitor;

        protected UserControlBase()
        {
            FlyoutElements = new List<FrameworkElement>(8);

            Loaded   += OnLoaded;
            Unloaded += OnUnloadedImpl;
        }
        #region OnUnloaded

        
        private void OnUnloadedImpl(object sender, RoutedEventArgs e)
        {

            if (!_init)
            {
                return;
            }

            _init = false;


            OnUnloadedInternal();

            //
            //
            OnUnloaded();
        }

        internal virtual void OnUnloadedInternal()
        {

        }

        protected virtual void OnUnloaded()
        {
        }

        #endregion

        #region IFlyoutElementRecipient

        void PollingFlyoutCompleted(object _)
        {
            //
            // 轮询Flyout元素是否附加完毕。
            if (_lastCount == FlyoutElements.Count)
            {
                _time++;
            }
            else
            {
                _lastCount = FlyoutElements.Count;
            }
            
            //
            // 每8ms扫描一次，若
            if (_time >= 20 && _monitor is not null)
            {
#if DEBUG // MARKS:      
         // Debug.WriteLine("正在轮询当前页面的Flyout元素完毕，需要Flyout()。");       
#endif
                //
                // 160ms间隔
                if (Ioc.IsRegistered<IAppModel>() && Ioc.Get<IAppModel>() is {} appModel)
                {
                    GUI.RunOnUIThread(() =>
                    {

                        if (appModel.ShouldFlyout(DataContext as ViewModelBase))
                        {
                            Flyout();
                        }
                    });
                }

                _time      = 0;
                _lastCount = 0;
                _monitor?.Dispose();
                _monitor = null;
            }
        }
        
        void IFlyoutElementRecipient.Clear()
        {
            FlyoutElements.Clear();
        }

        void IFlyoutElementRecipient.Accept(FrameworkElement element)
        {
            if (_monitor is null)
            {
                _lastCount = FlyoutElements.Count + 1;
                _monitor   = new Timer(PollingFlyoutCompleted, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(10));
            }

            FlyoutElements.Add(element);
        }

        #endregion


        #region Loaded

        internal void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_init)
            {
                return;
            }

            _init = true;

            //
            //
            var vm = ViewModel<ViewModelBase>();

            //
            //
            if (vm is not null)
            {
                if (vm.IsInitialized)
                {
                    vm.Resume();
                }
                else
                {
                    vm.Start();
                }
            }

            OnLoadedInternal();

            //
            //
            OnLoaded();
        }

        internal virtual void OnLoadedInternal()
        {

        }

        protected virtual void OnLoaded()
        {

        }

        #endregion

        #region Flyout System

        public void Flyout()
        {
            //
            // 需要将所有FrameworkElement按照步骤进行组装。

            var window     = Xaml.FindVisualParent<MTGWindow>(this);
            var collection = new List<FlyoutObject>(8);

            if (window is null)
            {
                return;
            }

            var dict = FlyoutElements.Select(x => new Tuple<int, FrameworkElement>(Controls.Flyout.GetIndex(x), x))
                                     .OrderBy(x => x.Item1)
                                     .Select(x => x.Item2)
                                     .ToList();

            foreach (var element in dict)
            {
                BuildFlyoutObject(element, collection);
            }

            if (DataContext is IFlyoutAmbientInitializer filter)
            {
                filter.BeforeExecute();
                WindowBehavior.Flyout(window, collection);
                filter.AfterExecute();
            }
            else
            {
                WindowBehavior.Flyout(window, collection);
            }
            
            //
            // 推送给IAppModel，基于ViewModel创建DummyData的机会。
            Ioc.Get<IAppModel>()
               .WhenFlyout(DataContext as ViewModelBase);
        }

        private void BuildFlyoutObject(FrameworkElement fe, List<FlyoutObject> collection)
        {
            var hint          = Controls.Flyout.GetHint(fe);
            var allowMultiple = Controls.Flyout.GetAllowMultiple(fe);
            var index         = Controls.Flyout.GetIndex(fe);
            var placement     = Controls.Flyout.GetPlacement(fe);

            if (string.IsNullOrEmpty(hint))
            {
                return;
            }


            if (allowMultiple)
            {
                var c = BuildFlyoutObjects(hint);

                if(c is null)
                {
                    return;
                }

                foreach (var wizard in c.Objects)
                {
                    wizard.Index     = index++;
                    wizard.View      = fe;
                    wizard.Placement = placement;

                    //
                    //
                    if (string.IsNullOrEmpty(wizard.Color))
                    {
                        wizard.Color = "#FF5868C8";
                    }


                    if (string.IsNullOrEmpty(wizard.ButtonText))
                    {
                        wizard.ButtonText = I18N.GetEnum(ButtonText.NextStep);
                    }

                    wizard.Index = index;

                    //
                    //
                    collection.Add(wizard);
                }
            }
            else
            {

                var obj = BuildFlyoutObject(hint);

                if(obj is null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(obj.Color))
                {
                    obj.Color = "#FF5868C8";
                }

                if (string.IsNullOrEmpty(obj.ButtonText))
                {
                    obj.ButtonText = I18N.GetEnum(ButtonText.NextStep);
                }

                obj.View      = fe;
                obj.Index     = index;
                obj.Placement = placement;

                //
                //
                collection.Add(obj);
            }


        }

        protected virtual FlyoutObject BuildFlyoutObject(string hint) => null;

        protected virtual MultiFlyoutObject BuildFlyoutObjects(string hint) => null;

        #endregion

        #region IBusyStateManager

        #endregion

        protected T ViewModel<T>() where T : ViewModelBase
        {
            return DataContext as T;
        }
    }
}