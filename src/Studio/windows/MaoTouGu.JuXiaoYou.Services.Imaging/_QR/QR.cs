// ----------------------------------------------------------
//            文件：QR.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 02:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells;
using ZXing;
using ZXing.QrCode;
using System.Runtime.InteropServices;
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static class QR
    {
        public static MemoryStream Generate(string json, int width = 300, int height = 300)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width        = width,
                    Height       = height,
                    Margin       = 1,
                    CharacterSet = "UTF-8",
                },
            };

            var pixelData = writer.Write(json);

            var ms     = new MemoryStream();
            var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height);
            var bitmapData = bitmap.LockBits(
                                             new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                                             System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                             System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            try
            {
                Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                             pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;


            return ms;
        }

        public static Task<BitmapImage> GenerateAsync(string json, int width = 300, int height = 300)
        {
            return Task.Run(async () =>
                            {
                                var ms  = Generate(json, width, height);
                                var cts = new TaskCompletionSource<BitmapImage>();

                                //
                                //
                                GUI.RunOnUIThread(() =>
                                                  {
                                                      var bmpImg = new BitmapImage();
                                                      bmpImg.BeginInit();
                                                      bmpImg.CacheOption  = BitmapCacheOption.OnLoad;
                                                      bmpImg.StreamSource = ms;
                                                      bmpImg.EndInit();
                                                      bmpImg.Freeze();

                                                      cts.SetResult(bmpImg);
                                                  });

                                //
                                //
                                var r = await cts.Task;

                                return r;
                            });
        }
    }
}