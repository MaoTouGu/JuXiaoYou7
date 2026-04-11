using System.Globalization;
using System.IO;
using Avalonia.Media.Imaging;
using Snoop.Infrastructure;

namespace KinonekoSoftware.UI
{
    partial class Xaml
    {
        
        //-------------------------------------------------------------
        //
        //                     ToColor / ToBrush
        //
        #region ToColor / ToBrush

        public static Color ToColor(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
            {
                return Colors.White;
            }

            // Remove the # if it exists.
            var hex = hexCode.TrimStart('#');

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
                    return new Color(
                                     255,
                                     byte.Parse(hex[..2], hexStyle),
                                     byte.Parse(hex.Substring(2, 2), hexStyle),
                                     byte.Parse(hex.Substring(4, 2), hexStyle));
                }

                if (hex.Length is 8)
                {
                    // Create a constant of the style we want (I don't want to type NumberStyles.HexNumber 4
                    // more times.)

                    // Parse Red, Green and Blue from each pair of characters.

                    // We are done, return the parsed colour.
                    return new Color(
                                     byte.Parse(hex[..2], hexStyle),
                                     byte.Parse(hex.Substring(2, 2), hexStyle),
                                     byte.Parse(hex.Substring(4, 2), hexStyle),
                                     byte.Parse(hex.Substring(6, 2), hexStyle));
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

        public static Bitmap ToBitmap(Stream stream)
        {
            return new Bitmap(stream);
        }

        public static Bitmap ToBitmap(byte[] buffer)
        {
            var stream = new MemoryStream(buffer);
            return ToBitmap(stream);
        }

        #endregion

        
        //-------------------------------------------------------------
        //
        //                     CaptureToStream / CaptureToBuffer
        //
        //-------------------------------------------------------------
        #region CaptureToStream / CaptureToBuffer



        public static RenderTargetBitmap Capture(Control target, int dpi = 100)
        {
            if (target is null)
            {
                return null;
            }

            var w = target.Bounds.Width;
            var h = target.Bounds.Height;
            // var dpi  = target.GetDpi(target);
            var dpiX = 96d * dpi / 100; //* dpi.DpiScaleX;
            var dpiY = 96d * dpi / 100; //* dpi.DpiScaleY;

            //
            // 暂时不知道Avalonia需不需要Border参与
            // if (target is not Border root)
            // {
            //     //
            //     // Windows 平台截图工作需要有Border参与,所以需要创建一个Dummy Border
            //     root = new Border
            //     {
            //         Background = new VisualBrush
            //         {
            //             Visual = target
            //         },
            //         Width  = w,
            //         Height = h
            //     };
            //
            //     target.Measure(new Size(w, h));
            //     target.Arrange(new Rect(0, 0, w, h));
            // }


            //
            // 创建RenderTargetBitmap
            var pixelSize = new PixelSize((int)(w * dpiX / 96), (int)(h * dpiY / 96));
            var dpiVec    = new Vector(dpiX, dpiY);
            var bitmap    = new RenderTargetBitmap(pixelSize, dpiVec);


            bitmap.Render(target);
            return bitmap;
        }


        public static MemoryStream CaptureToStream(Control target, int dpi = 100)
        {
            if (target is null)
            {
                return null;
            }

            var bitmap = Capture(target, dpi);
            var ms     = new MemoryStream();
            bitmap.Save(ms);

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static byte[] CaptureToBuffer(Control target, int dpi = 96) => CaptureToStream(target, dpi).ToArray();

        #endregion


        //
        // TODO:
        // public static object FindResource(Type key)
        // {
        //     return Application.Current.Resources[new DataTemplateKey(key)];
        // }
        //
        // TODO:
        // public static object FindResource(string key)
        // {
        //     return Application.Current.Resources[key];
        // }
        //
        //
        // TODO:
        // public static T FindResource<T>(string key) where T : class
        // {
        //     return Application.Current.Resources[key] as T;
        // }
        //
        // TODO:
        // public static object Find(string key)
        // {
        //     return Application.Current.Resources[key];
        // }
    }
}