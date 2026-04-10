namespace MaoTouGu.Shells.AppModels
{
    public class DockingModel : AppModelBase
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
        
        public override void Navigate(PageBase page)
        {
            throw new NotImplementedException();
        }
        
    }
}