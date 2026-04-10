using System.Windows.Media.TextFormatting;

namespace KinonekoSoftware.UI.Controls
{
    public abstract class TextControlBase : Control
    {
        static TextControlBase()
        {
            FontSizeProperty.AddOwner(typeof(TextControlBase), new UIPropertyMetadata(OnFontSizeChanged));
            FontFamilyProperty.AddOwner(typeof(TextControlBase), new UIPropertyMetadata(OnFontFamilyChanged));
            FontWeightProperty.AddOwner(typeof(TextControlBase), new UIPropertyMetadata(OnFontWeightChanged));
            FontStretchProperty.AddOwner(typeof(TextControlBase), new UIPropertyMetadata(OnFontStretchChanged));
            FontStyleProperty.AddOwner(typeof(TextControlBase), new UIPropertyMetadata(OnFontStyleChanged));
        }

        #region TypefaceChanged

        
        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextControlBase)?.OnTypefaceChanged();
        }

        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextControlBase)?.OnTypefaceChanged();
        }

        private static void OnFontWeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextControlBase)?.OnTypefaceChanged();
        }

        private static void OnFontStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextControlBase)?.OnTypefaceChanged();
        }

        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TextControlBase)?.OnTypefaceChanged();
        }

        #endregion


        protected TextControlBase()
        {
            Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            Dpi      = VisualTreeHelper.GetDpi(this).PixelsPerDip;
        }
        
        #region OnTypefaceChanged

        internal void OnTypefaceChangedInternal()
        {
            Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            Dpi      = VisualTreeHelper.GetDpi(this).PixelsPerDip;

            //
            //
            OnTypefaceChanged();
            InvalidateMeasure();
            InvalidateVisual();
        }
        
        protected virtual void OnTypefaceChanged()
        {
            Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            Dpi      = VisualTreeHelper.GetDpi(this).PixelsPerDip;

            //
            //
            InvalidateMeasure();
            InvalidateVisual();
        }
        

        #endregion

        #region CreateFormattedText


        protected FormattedText CreateFormattedText(string text)
        {
            return new FormattedText(text,
                                     CultureInfo.CurrentUICulture,
                                     FlowDirection,
                                     Typeface,
                                     FontSize,
                                     Foreground,
                                     Dpi);
        }
        
        protected FormattedText CreateFormattedText(string text, double width)
        {
            return new FormattedText(text,
                                     CultureInfo.CurrentUICulture,
                                     FlowDirection,
                                     Typeface,
                                     FontSize,
                                     Foreground,
                                     Dpi)
            {
                MaxTextWidth  = width,
            };
        }
        
        protected FormattedText CreateFormattedText(string text, double width, double height)
        {
            return new FormattedText(text,
                                     CultureInfo.CurrentUICulture,
                                     FlowDirection,
                                     Typeface,
                                     FontSize,
                                     Foreground,
                                     Dpi)
            {
                MaxTextHeight = height,
                MaxTextWidth  = width,
            };
        }
        

        #endregion

        protected double Dpi { get; private set; }

        protected Typeface Typeface { get; private set; }
    }
}