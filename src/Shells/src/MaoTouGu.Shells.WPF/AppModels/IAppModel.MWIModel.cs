namespace MaoTouGu.Shells.AppModels
{
    public abstract class MultipleWindowModel : AppModelBase
    {
        protected readonly Dictionary<int, MultipleWindowContext> WindowTable = new();
        protected readonly Queue<PageBase> PendingQueue = new();
        protected bool IsStartingNewWindow;
        
        #region GetDialogHost

        private DialogHost GetDialogHostImpl(ViewModelBase target)
        {
            object cache;

            if (target is DialogBase db)
            {
                cache = GetParentView(db);
            }
            else
            {
                cache = GetViewCache(target);

            }

            if (cache is not FrameworkElement fe)
            {
                return null;
            }

            var host = Xaml.FindVisualParent<DialogHost>(fe);

            return host;
        }

        public sealed override IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target) => GetDialogHostImpl(target);

        public sealed override IDialogService GetDialogHost(ViewModelBase target) => GetDialogHostImpl(target);

        #endregion

        #region Activate / Deactivate

        public sealed override void Activate(Window window)
        {
            if (window is null)
            {
                return;
            }

            if (WindowTable.TryGetValue(window.GetHashCode(), out var ctx))
            {
                ctx.IsActivate = true;
            }
        }

        public sealed override void Deactivate(Window window)
        {
            if (window is null)
            {
                return;
            }

            if (WindowTable.TryGetValue(window.GetHashCode(), out var ctx))
            {
                ctx.IsActivate = false;
            }
        }

        #endregion

        #region Navigate

        public override async Task<bool> Navigate(PageBase page, params object[] args)
        {
            if (!await page.ReceiveInternal(args))
            {
                return false;
            }
            
            if (!CanNavigateFixed(page, out var theSameOne))
            {
                if (InstanceTable.TryGetValue(theSameOne.GetHashCode(), out var ctx))
                {
                    var wnd = ctx.Window;

                    if (WindowTable.TryGetValue(wnd.GetHashCode(), out var ctx2))
                    {
                        ctx2.SetPage(theSameOne, false);
                    }

                    var last = wnd.WindowState;
                    wnd.WindowState = WindowState.Minimized;
                    wnd.Activate();
                    wnd.WindowState = last;
                }

                page.Dispose();
                return false;
            }
            
            GUI.RunOnUIThread(() =>
                              {
                                  MultipleWindowContext ctx;

                                  if (page is IHostedWindowNavigation)
                                  {
                                      ctx = FindMainWindowContentHost();

                                  }
                                  else
                                  {
                                      ctx = FindActivatedWindowContentHost();
                                  }

                                  if (ctx is null)
                                  {
                                      //
                                      // 等待窗口创建完成后自动完成导航。
                                      PendingQueue.Enqueue(page);
                
                                      //
                                      // 创建一个新的WindowContentHost
                                      if (!IsStartingNewWindow)
                                      {
                                          var window = CreateNewWindowContentHost();
                                          window.Show();
                                          IsStartingNewWindow = true;
                                      }
                                  }
                                  else
                                  {
                                      
                                      ctx.SetPage(page);
                                      ctx.Tabs.Add(page);
                                  }

                              });

            return true;
        }

        protected abstract Window CreateNewWindowContentHost();

        protected abstract bool IsMainWindow(Window window);

        protected MultipleWindowContext FindActivatedWindowContentHost()
        {
            return WindowTable.Values
                              .FirstOrDefault(x => x.IsActivate && !IsMainWindow(x.Window));
        }

        protected MultipleWindowContext FindMainWindowContentHost()
        {
            return WindowTable.Values
                              .FirstOrDefault(x => IsMainWindow(x.Window));
        }

        #endregion

        #region OnAttach / OnDetach

        
        protected virtual void OnAttach(Window window)
        {
            
        }

        protected virtual void OnDetach(Window window)
        {

        }

        #endregion

        #region OnAppModelControlInitialized / ReducePendingQueue /DetachOverride
        
        protected sealed override void OnAppModelControlInitialized(Window window, DialogHost dialogHost, ContentHost contentHost)
        {
            if (window is null)
            {
                return;
            }
            
            
            var ctx = new MultipleWindowContext
            {
                Window      = window,
                ContentHost = contentHost,
                DialogHost  = dialogHost,
            };

            if (WindowTable.TryAdd(window.GetHashCode(), ctx))
            {
                window.DataContext = ctx;
                ReducePendingQueue(window, ctx);
                OnAttach(window);
            }
        }

        private async void ReducePendingQueue(Window window, MultipleWindowContext ctx)
        {
            if (IsMainWindow(window) || PendingQueue.Count == 0)
            {
                return;
            }
            
            await Task.Delay(100);
            GUI.RunOnUIThread(() =>
                              {
                                  while (PendingQueue.Count > 0)
                                  {
                                      var page = PendingQueue.Dequeue();
                                      
                                      //
                                      //
                                      ctx.SetPage(page);
                                      ctx.Tabs.Add(page);
                                  }
                                  
                                  IsStartingNewWindow = false;
                              });
        }
        

        protected sealed override void DetachOverride(Window window)
        {
            if (window is null)
            {
                return;
            }

            if (WindowTable.Remove(window.GetHashCode()))
            {
                window.DataContext = null;
                OnDetach(window);
            }
        }

        #endregion

        public sealed override void Notify(Notification notification)
        {
            WindowTable.Values
                       .Where(x => x.IsActivate)
                       .Select(x => x.DialogHost)
                       .FirstOrDefault()
                       ?.Notify(notification);
        }

        protected override void OnViewDisconnected(PageContext ctx)
        {
            var window = ctx.Window;

            if (window is null || !WindowTable.TryGetValue(ctx.Window.GetHashCode(), out var context))
            {
                return;
            }

            if (ctx.ViewModel is not PageBase p)
            {
                return;
            }

            var index = context.Tabs.IndexOf(p);
            context.Tabs.RemoveAt(index);
            
            if (context.Tabs.Count == 0)
            {
                context.Window?.Close();
                return;
            }
            
            if (context.Page == p)
            {
                if (context.Tabs.Count > index)
                {
                    context.SetPage(context.Tabs[index], false);
                }
                else
                {
                    context.SetPage(context.Tabs[^1], false);
                }
            }

            context.SetPage(context.Tabs[^1], false);
        }
    }
}