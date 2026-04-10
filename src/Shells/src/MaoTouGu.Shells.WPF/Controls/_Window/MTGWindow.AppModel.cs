namespace MaoTouGu.Shells.Controls
{
    partial class MTGWindow
    {
        #region OnActivated / OnDeactivated

        
        protected sealed override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            AppModel.Activate(this);
            OnActivatedOverride(e);
        }

        protected virtual void OnActivatedOverride(EventArgs e)
        {
            
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            AppModel.Deactivate(this);
            OnDeactivatedOverride(e);
        }
        
        protected virtual void OnDeactivatedOverride(EventArgs e)
        {
        }


        #endregion

        internal AppModelBase AppModel => Ioc.Get<IAppModel>() as AppModelBase;
    }
}