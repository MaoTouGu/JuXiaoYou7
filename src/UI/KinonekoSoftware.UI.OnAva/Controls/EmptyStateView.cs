using Avalonia;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;

namespace KinonekoSoftware.UI.Controls
{
    [PseudoClasses("on", "off")]
    public sealed class EmptyStateView : ContentControl
    {
        public static readonly StyledProperty<DataTemplate>         EmptyStateTemplateProperty;
        public static readonly StyledProperty<object>               EmptyStateProperty;
        public static readonly StyledProperty<bool>                 IsEmptyProperty;
        public static readonly StyledProperty<bool>                 NotEmptyProperty;
        public static readonly DirectProperty<EmptyStateView, bool> IsEmptyVisibleProperty;

        private bool             _isEmptyVisible;
        private ContentPresenter PART_Content;
        private ContentPresenter PART_EmptyState;

        static EmptyStateView()
        {
            IsEmptyVisibleProperty     = AvaloniaProperty.RegisterDirect<EmptyStateView, bool>(nameof(IsEmptyVisible), x => x._isEmptyVisible);
            IsEmptyProperty            = AvaloniaProperty.Register<EmptyStateView, bool>(nameof(IsEmpty));
            NotEmptyProperty           = AvaloniaProperty.Register<EmptyStateView, bool>(nameof(NotEmpty));
            EmptyStateProperty         = AvaloniaProperty.Register<EmptyStateView, object>(nameof(EmptyState));
            EmptyStateTemplateProperty = AvaloniaProperty.Register<EmptyStateView, DataTemplate>(nameof(EmptyStateTemplate));

            IsEmptyVisibleProperty.Changed.Subscribe(x =>
            {
                //
                //
                var sender = (EmptyStateView)x.Sender;
                
                //
                //
                
                if (x.GetNewValue<bool>())
                {
                    sender.OnEmpty();
                    sender.PseudoClasses.Set("off", true);
                    sender.PseudoClasses.Set("on", false);
                }
                else
                {
                    sender.OnNotEmpty();
                    sender.PseudoClasses.Set("off", false);
                    sender.PseudoClasses.Set("on", true);
                }
            });

            IsEmptyProperty.Changed.Subscribe(x =>
            {
                //
                //
                var sender = (EmptyStateView)x.Sender;

                sender.IsEmptyVisible = x.GetNewValue<bool>();
            });
            
            
            NotEmptyProperty.Changed.Subscribe(x =>
            {
                //
                //
                var sender = (EmptyStateView)x.Sender;

                sender.IsEmptyVisible = !x.GetNewValue<bool>();
            });
            
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            PART_Content    = e.NameScope.Get<ContentPresenter>("PART_Content");
            PART_EmptyState = e.NameScope.Get<ContentPresenter>("PART_EmptyState");
        }

        private void OnEmpty()
        {
            PART_Content.IsVisible    = false;
            PART_EmptyState.IsVisible = true;
        }
        
        private void OnNotEmpty()
        {
            PART_Content.IsVisible    = true;
            PART_EmptyState.IsVisible = false;
        }

        #region EmptyState

        public bool IsEmptyVisible
        {
            get => GetValue(IsEmptyVisibleProperty);
            private set
            {
                SetAndRaise(IsEmptyVisibleProperty, ref _isEmptyVisible, value);
            }
        }

        public bool IsEmpty
        {
            get => GetValue(IsEmptyProperty);
            set
            {
                SetValue(IsEmptyProperty, value);
            }
        }

        public bool NotEmpty
        {
            get => GetValue(NotEmptyProperty);
            set
            {
                SetValue(NotEmptyProperty, value);
            }
        }


        public DataTemplate EmptyStateTemplate
        {
            get => GetValue(EmptyStateTemplateProperty);
            set => SetValue(EmptyStateTemplateProperty, value);
        }


        public object EmptyState
        {
            get => GetValue(EmptyStateProperty);
            set => SetValue(EmptyStateProperty, value);
        }

        #endregion
    }
}