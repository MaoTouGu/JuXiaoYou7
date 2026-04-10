namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class GhostButton : ButtonBase
    {
        public static readonly StyledProperty<bool> ShowContextMenuProperty =
                AvaloniaProperty.Register<GhostButton,bool>(nameof(ShowContextMenu));
        
        public bool ShowContextMenu
        {
            get => GetValue(ShowContextMenuProperty);
            set => SetValue(ShowContextMenuProperty, value);
        }

        
        protected override void OnClick()
        {
            if (ShowContextMenu && ContextMenu is not null)
            {
                ContextMenu.PlacementTarget = this;
                ContextMenu.Placement       = PlacementMode.Pointer;
                ContextMenu.Open();
            }
            base.OnClick();
        }
    }
}