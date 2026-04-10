using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using Snoop.Infrastructure;

namespace KinonekoSoftware.UI
{
    partial class Xaml
    {
        
        //-------------------------------------------------------------
        //
        //                     ToColor / ToBrush
        //
        //-------------------------------------------------------------
        #region ToColor / ToBrush

        public static Color ToColor(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
            {
                return Colors.White;
            }

            // Remove the # if it exists.
            var hex = hexCode.TrimStart('#');

            // Create the colour that we will work on.
            var colour = new Color();

            // If we are working with the shorter hex colour codes, duplicate each character as per the
            // spec https://www.w3.org/TR/2001/WD-css3-color-20010305#colorunits
            // (From E3F to EE33FF)
            if (hex.Length is 3 or 4)
            {
                var longHex = "";

                // For each character in the short hex code add two to the long hex code.
                foreach (var t in hex)
                {
                    longHex += t;
                    longHex += t;
                }

                // the short hex is now the long hex.
                hex = longHex;
            }

            try
            {
                const NumberStyles hexStyle = NumberStyles.HexNumber;

                // We should be working with hex codes that are 6 or 8 characters long.
                if (hex.Length is 6)
                {
                    // Create a constant of the style we want (I don't want to type NumberStyles.HexNumber 4
                    // more times.)
                    colour.R = byte.Parse(hex[..2], hexStyle);
                    colour.G = byte.Parse(hex.Substring(2, 2), hexStyle);
                    colour.B = byte.Parse(hex.Substring(4, 2), hexStyle);

                    // We are done, return the parsed colour.
                    colour.A = 255;
                    return colour;
                }

                if (hex.Length is 8)
                {
                    // Create a constant of the style we want (I don't want to type NumberStyles.HexNumber 4
                    // more times.)

                    // Parse Red, Green and Blue from each pair of characters.
                    colour.A = byte.Parse(hex[..2], hexStyle);
                    colour.R = byte.Parse(hex.Substring(2, 2), hexStyle);
                    colour.G = byte.Parse(hex.Substring(4, 2), hexStyle);
                    colour.B = byte.Parse(hex.Substring(6, 2), hexStyle);

                    // We are done, return the parsed colour.
                    return colour;
                }
            }
            catch
            {
                return Colors.White;
            }

            return Colors.White;
        }

        public static SolidColorBrush ToBrush(string value)
        {
            return new SolidColorBrush(ToColor(value));
        }

        #endregion

        
        //-------------------------------------------------------------
        //
        //                     ToBitmap
        //
        //-------------------------------------------------------------
        #region ToBitmap

        public static BitmapImage ToBitmap(Stream stream)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            if (bi.CanFreeze) bi.Freeze();
            return bi;
        }

        public static BitmapImage ToBitmap(byte[] buffer)
        {
            var stream = new MemoryStream(buffer);
            var bi     = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            if (bi.CanFreeze) bi.Freeze();
            return bi;
        }

        #endregion

        
        //-------------------------------------------------------------
        //
        //                     CaptureToStream / CaptureToBuffer
        //
        //-------------------------------------------------------------
        #region CaptureToStream / CaptureToBuffer

        public static MemoryStream CaptureToStream(FrameworkElement target, int dpi = 96)
        {
            if (target is null)
            {
                return null;
            }

            var bitmap  = VisualCaptureUtil.SaveVisual(target, dpi);
            var ms      = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(ms);

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static RenderTargetBitmap Capture(FrameworkElement target, int dpi = 96)
        {
            if (target is null)
            {
                return null;
            }

            return VisualCaptureUtil.SaveVisual(target, dpi);
        }
        public static byte[] CaptureToBuffer(FrameworkElement target, int dpi = 96)
        {
            if (target is null)
            {
                return null;
            }

            var bitmap  = VisualCaptureUtil.SaveVisual(target, dpi);
            var ms      = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(ms);

            //
            //
            var buffer = ms.ToArray();

            //
            //
            ms.Dispose();

            //
            //
            return buffer;
        }

        #endregion
        
        public static object FindResource(Type key)
        {
            return Application.Current.Resources[new DataTemplateKey(key)];
        }
        
        public static object FindResource(string key)
        {
            return Application.Current.Resources[key];
        }
        
        
        public static T FindResource<T>(string key) where T : class
        {
            return Application.Current.Resources[key] as T;
        }

        public static object Find(string key)
        {
            return Application.Current.Resources[key];
        }
    }
}