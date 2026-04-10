using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Reactive;
using Avalonia.Xaml.Interactivity;
using ColorPicker.Behaviors;
using ColorPicker.Converters;
using ColorPicker.Models;

namespace ColorPicker
{
    [TemplatePart(Name = "PART_ResourcesContainer", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class HexColorTextBox : PickerControlBase
    {
        public static readonly StyledProperty<bool> ShowAlphaProperty = AvaloniaProperty.Register<HexColorTextBox, bool>(
                                                                                                                         nameof(ShowAlpha), true);

        public bool ShowAlpha
        {
            get => GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }
    
        public static readonly StyledProperty<HexRepresentationType> HexRepresentationProperty = 
            AvaloniaProperty.Register<HexColorTextBox, HexRepresentationType>(
                                                                              nameof(HexRepresentation), HexRepresentationType.RGBA);

        public HexRepresentationType HexRepresentation
        {
            get => GetValue(HexRepresentationProperty);
            set => SetValue(HexRepresentationProperty, value);
        }

        private void OnChanged(object sender, EventArgs e)
        {
            if (_grid is null)
            {
                return;
            }
            
            if (_textBox is null)
            {
                return;
            }
            if (_colorToHexConverter is null)
            {
                return;
            }

            if (_colorToHexConverter.Convert(SelectedColor, null, null, null) is string converted)
            {
                _textBox.Text = converted;
            }
        }

        private TextBox             _textBox;
        private Grid                _grid;
        private ColorToHexConverter _colorToHexConverter;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
        
            // I feel like I'm terribly abusing the PART_ functionality and that I'm creating insanely non-obvious code
            // But I can't think of any other way of updating the text of the text box when ShowAlpha and HexRepresentation change

            _grid    = e.NameScope.Find<Grid>("PART_ResourcesContainer");
            _textBox = e.NameScope.Find<TextBox>("PART_TextBox");

            if (_grid is null)
            {
                return;
            }
            
            if (_textBox is null)
            {
                return;
            }
            
            if (!_grid.Resources.TryGetValue("ColorToHexConverter", out var converter) || converter is not ColorToHexConverter colorToHexConverter)
            {
                return;
            }

            _colorToHexConverter = colorToHexConverter;

        
            colorToHexConverter.OnShowAlphaChange             += OnChanged;
            colorToHexConverter.OnShowHexRepresentationChange += OnChanged;        
        }
    }
}