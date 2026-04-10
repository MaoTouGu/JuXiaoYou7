// ----------------------------------------------------------
//            文件：Crop.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 15:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;
using Size = SixLabors.ImageSharp.Size;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static class Crop
    {
        internal static async Task<Result<ImageResult>> OnGravatarWasCropped(ImageEditorCallback expr, byte[] buffer, int h, int w)
        {
            var vm = new ImageEditorViewModel(buffer);
            var r  = await expr(vm);

            if (!r.IsFinished)
            {
                return Result<ImageResult>.Failure;
            }

            var r2 = await CropAsync(
                                     buffer,
                                     w,
                                     h,
                                     new Rectangle(r.Value.Item1,
                                                   r.Value.Item2,
                                                   r.Value.Item3,
                                                   r.Value.Item4));
            
            return Result<ImageResult>.Success(r2);
        }
        
        public static async Task<ImageResult> CropAsync(byte[] buffer, int w, int h, Rectangle rect)
        {
            var img = Image.Load<Rgba32>(buffer);

            img.Configuration
               .MemoryAllocator
               .ReleaseRetainedResources();

            var ms = new MemoryStream(buffer.Length);

            img.Mutate(d =>
                       {
                           d.Crop(rect);

                           if (rect.Width > 256 || rect.Height > 256)
                           {
                               d.Resize(new Size(256, 256));
                           }
                       });


            await img.SaveAsPngAsync(ms);
            img.Dispose();

            var array = ms.ToArray();
            ms.Seek(0, SeekOrigin.Begin);

            return new ImageResult
            {
                Id           = Guid.NewGuid().ToString("N"),
                Buffer       = array,
                OriginHeight = h,
                OriginWidth  = w,
                Width        = rect.Width,
                Height       = rect.Height,
            };
        }
    }
}