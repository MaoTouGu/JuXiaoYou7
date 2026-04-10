namespace MaoTouGu.Shells.AppModels
{
    public abstract class DockingModel : AppModelBase
    {
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
        
        public override async Task<bool> Navigate(PageBase page, params object[] args)
        {
            if (!await page.ReceiveInternal(args))
            {
                return false;
            }
            
            if (CanNavigateFixed(page, out var theSameOne))
            {
                return true;
            }

            return false;
        }
        
    }
}