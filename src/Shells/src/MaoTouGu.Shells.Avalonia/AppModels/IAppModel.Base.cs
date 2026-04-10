namespace MaoTouGu.Shells.AppModels
{
    public abstract class AppModelBase : Lifetime, IAppModel
    {
        protected readonly Dictionary<int, PageContext> InstanceTable = new Dictionary<int, PageContext>();

        #region IWorkspaceAmbient

        
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

            }
        }

        public void UnsetViewCache(ViewModelBase target)
        {
            if (target is null)
            {
                return;
            }

            if (InstanceTable.Remove(target.GetHashCode(), out var ctx))
            {
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
        
        public IGuideService GetGuideService(ViewModelBase target) => GetViewCache(target) as IGuideService;

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
        
        
        public abstract void Navigate(PageBase page);
        public abstract IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target);
        public abstract IDialogService GetDialogHost(ViewModelBase target);
        
        protected class PageContext
        {
            private FrameworkElement _view;
            private ViewModelBase    _viewModel;
            private ViewModelBase    _parent;

            public PageContext(FrameworkElement v, ViewModelBase vm, ViewModelBase parent = null)
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
                get
                {
                    if (field is null)
                    {
                        Debug.Assert(_view is not null);

                        //
                        // 寻找视觉父级。
                        field = Xaml.FindVisualParent<Window>(_view);
                    }

                    return field;
                }
            }
        }
    }
}