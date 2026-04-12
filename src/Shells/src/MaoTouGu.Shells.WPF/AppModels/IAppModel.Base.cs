using System.Diagnostics.CodeAnalysis;

namespace MaoTouGu.Shells.AppModels
{
    public abstract class AppModelBase : FlyoutAmbient, IAppModel, IAppModelInitialized, IDisposableCollector
    {
        protected readonly Dictionary<int, PageContext> InstanceTable = new Dictionary<int, PageContext>();

        private readonly Dictionary<int, InitializeCache> _initializeCaches = new();

        public void Collect(IDisposable disposable)
        {
            DisposableCollection.Collect(disposable);
        }

        #region Activate / Deactivate

        
        public virtual void Activate(Window window)
        {
        }
        
        public virtual void Deactivate(Window window)
        {
        }

        #endregion
        
        protected virtual void OnViewConnected(PageContext ctx)
        {
            
        }
        
        protected virtual void OnViewDisconnected(PageContext ctx)
        {
            
        }
        
        public abstract void Notify(Notification notification);

        #region Close


        public void Close(Window window)
        {
            if (window is null)
            {
                return;
            }

            var theSameWindowContext = InstanceTable.Where(x => x.Value.Window == window)
                                                    .ToList();

            foreach (var (k, v) in theSameWindowContext)
            {
                UnsetViewCache(k, v.ViewModel);
            }
        }

        #endregion

        #region Initialized

        void Initialize(int hashCode, InitializeCache cache)
        {
            _initializeCaches.Remove(hashCode);
            OnAppModelControlInitialized(cache.Window, cache.DialogHost, cache.ContentHost);
            Start();
        }
        
        /// <summary>
        /// 用于简化AppModel的设计。
        /// </summary>
        /// <param name="window"></param>
        /// <param name="dialogHost"></param>
        /// <param name="contentHost"></param>
        protected abstract void OnAppModelControlInitialized(Window window, DialogHost dialogHost, ContentHost contentHost);

        void IAppModelInitialized.Attach(Window window)
        {
            if (window is null)
            {
                return;
            }

            var hashCode = window.GetHashCode();
            
            if (_initializeCaches.TryGetValue(hashCode, out var cache))
            {
                if (!cache.IsReady)
                {
                    return;
                }
                
                Initialize(hashCode, cache);
            }

            cache = new InitializeCache
            {
                Window = window,
            };

            _initializeCaches.TryAdd(hashCode, cache);
        }

        void IAppModelInitialized.Attach(Window window, DialogHost host)
        {
            if (window is null || host is null)
            {
                return;
            }
            
            var hashCode = window.GetHashCode();
            
            if (_initializeCaches.TryGetValue(hashCode, out var cache))
            {
                cache.DialogHost = host;
            }
            else
            {
                cache = new InitializeCache
                {
                    Window     = window,
                    DialogHost = host,
                };
                _initializeCaches.TryAdd(hashCode, cache);
            }
            
            if (cache.IsReady)
            {
                Initialize(hashCode, cache);
            }
            
        }
        
        void IAppModelInitialized.Attach(Window window, ContentHost host)
        {
            if (window is null || host is null)
            {
                return;
            }
            
            var hashCode = window.GetHashCode();
            
            if (_initializeCaches.TryGetValue(hashCode, out var cache))
            {
                cache.ContentHost = host;
            }
            else
            {
                cache = new InitializeCache
                {
                    Window      = window,
                    ContentHost = host,
                };
                _initializeCaches.TryAdd(hashCode, cache);
            }
            
            if (cache.IsReady)
            {
                Initialize(hashCode, cache);
            }
        }

        void IAppModelInitialized.Detach(Window window)
        {
            DetachOverride(window);
        }
        
        protected virtual void DetachOverride(Window window)
        {
        }
        #endregion
        
        #region IWorkspaceAmbient

        public void SetWindow(ViewModelBase target, object window)
        {
            if (target is null)
            {
                return;
            }
            
            if (window is not Window w)
            {
                return;
            }

            if (InstanceTable.TryGetValue(target.GetHashCode(), out var context))
            {
                context.Window = w;
            }
        }

        public void SetViewCache(ViewModelBase target, object view, ViewModelBase parent = null)
        {
            if (target is null || view is null)
            {
                return;
            }

            var ctx = new PageContext((FrameworkElement)view, target, parent);

            //
            //
            if (InstanceTable.TryAdd(target.GetHashCode(), ctx))
            {
                OnViewConnected(ctx);
            }
        }

        private void UnsetViewCache(int hashCode, ViewModelBase target)
        {
            if (InstanceTable.Remove(hashCode, out var ctx))
            {
                OnViewDisconnected(ctx);
                
                if (target is not PageBase)
                {
                    //
                    // 需要移除所有Dialog
                    return;
                }

                var children = InstanceTable.Values
                                            .Where(x => x.Parent == target)
                                            .ToList();

                if (children is null || children.Count == 0)
                {
                    return;
                }

                foreach (var context in children)
                {
                    context.ViewModel.Dispose();
                }
            }
        }

        public void UnsetViewCache(ViewModelBase target)
        {
            if (target is null)
            {
                return;
            }

            UnsetViewCache(target.GetHashCode(), target);
        }
        
        public IFlyoutService GetFlyoutService(ViewModelBase target) => GetViewCache(target) as IFlyoutService;

        public object GetParentView(DialogBase target)
        {
            if (target is null)
            {
                return null;
            }

            if (InstanceTable.TryGetValue(target.GetHashCode(), out var ctx))
            {
                return GetViewCache(ctx.Parent);
            }

            return null;
        }

        public object GetViewCache(ViewModelBase target)
        {
            if (target is null)
            {
                return null;
            }

            if (InstanceTable.TryGetValue(target.GetHashCode(), out var ctx))
            {
                return ctx.View;
            }

            return null;
        }

        #endregion

        #region Navigate

        [SuppressMessage("ReSharper", "DuplicatedSequentialIfBodies")]
        protected bool CanNavigateFixed(PageBase target,out PageBase theSameOne)
        {
            var targetType = target.GetType();
            var result     = true;

            theSameOne = null;
            
            foreach (var instance in InstanceTable.Values
                                                  .Select(x => x.ViewModel)
                                                  .OfType<PageBase>())
            {
                //
                //
                theSameOne = instance;
                
                var instanceType = instance.GetType();

                //
                //
                if (instanceType != targetType)
                {
                    continue;
                }
                
                //
                // 任意一个不是单例的场景都视为无法导航。
                if (target.Singleton || instance.Singleton)
                {
                    
                    if (!string.IsNullOrEmpty(target.InstanceID)   &&
                        !string.IsNullOrEmpty(instance.InstanceID) &&
                        instance.InstanceID != target.InstanceID)
                    {
                        result = false;
                        break;
                    }

                    if (target.Singleton && instance.Singleton)
                    {
                        result = false;
                        break;
                    }
                }

                //
                // 如果是可删除页面，则InstanceID相等的场合视为无法导航。
                if (target.Removable && instance.InstanceID == target.InstanceID)
                {
                    result = false;
                    break;
                }
                    
                //
                // 任意一个是不可删除页面时，都视为单例页面。无法导航。
                if(!target.Removable || !instance.Removable)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        
        [Obsolete]
        protected bool CanNavigate(PageBase target)
        {
            //
            // 可删除且不为单例，则返回true
            if (target.Removable && !target.Singleton)
            {
                return true;
            }

            //
            // 不可删除 或者 为单例 -> 判断是否已经存在了
            var a = (!target.Removable || target.Singleton);
            var b = InstanceTable.Values.All(x => x.ViewModel.GetType() != target.GetType());
            
            //
            // 不是单例就需要判断InstanceID是否相同

            return a && b;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Task<bool> Navigate<T>() where T : PageBase => Navigate(ClassStatic.CreateInstance<T>());
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Task<bool> Navigate<T>(params object[] args) where T : PageBase => Navigate(ClassStatic.CreateInstance<T>(), args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public Task<bool> Navigate(PageBase page) => Navigate(page, null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="args"></param>
        public abstract Task<bool> Navigate(PageBase page, params object[] args);

        #endregion
        
        /// <summary>
        /// 获取一个<see cref="IBusyStateRecipient"/>接口，这咒文由<see cref="DialogHost"/>实现。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract IDialogService GetDialogHost(ViewModelBase target);
        
        protected class PageContext
        {
            private FrameworkElement _view;
            private ViewModelBase    _viewModel;
            private ViewModelBase    _parent;
            private Window           _window;

            public PageContext(FrameworkElement v, ViewModelBase vm,  ViewModelBase parent = null)
            {
                _view      = v;
                _viewModel = vm;
                _parent    = parent;
            }

            public FrameworkElement View      => _view;
            public ViewModelBase    ViewModel => _viewModel;
            public ViewModelBase    Parent    => _parent;

            /// <summary>
            /// 获取当前页面上下文归属的Window，仅限WPF。
            /// </summary>
            public Window Window
            {
                get => _window;
                internal set => _window = value;
            }
        }

        class InitializeCache
        {
            internal bool IsReady => Window is not null     &&
                                     DialogHost is not null &&
                                     ContentHost is not null;
            
            internal Window      Window;
            internal DialogHost  DialogHost;
            internal ContentHost ContentHost;
        }

        public DisposableCollection DisposableCollection { get; } = new DisposableCollection();
    }
}