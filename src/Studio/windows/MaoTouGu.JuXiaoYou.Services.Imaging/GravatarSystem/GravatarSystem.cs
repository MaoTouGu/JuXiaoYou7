// ----------------------------------------------------------
//            文件：GravatarSystem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 15:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells;
using MaoTouGu.Shells.Core;
using MaoTouGu.Shells.Interops;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static class GravatarSystem
    {
        public static async Task<Result<ImageResult>> PickGravatar(PageBase context)
        {
            //
            // 通过iFS接口选择文件。
            var r = Interop.OpenFileAsync(SR.Image_All);

            if (!r.IsFinished)
            {
                return Result<ImageResult>.Failure;
            }

            
            try
            {
                var buffer = await File.ReadAllBytesAsync(r.Value);

                if (!ImageInfo.GetMetadata(buffer, out var w, out var h))
                {
                    return Result<ImageResult>.Failed("不支持此文件格式。");
                }
                
                if (w <= 32 || h <= 32)
                {
                    return Result<ImageResult>.Failed("图片尺寸太小。");
                }

                if (w != h)
                {
                    return await Crop.OnGravatarWasCropped(context.Object, buffer, h, w);
                }
                
                if (w < 256)
                {
                    return Result<ImageResult>.Success(await SaveToPNG(buffer));
                }

                var option = await context.Query("裁切图片", "当前图片可以裁切，请问您是选择直接使用还是裁切后使用?", "直接使用", "裁切");

                if (option == TripleOption.Cancel)
                {
                    return Result<ImageResult>.Failure;
                }

                if (option == TripleOption.Option1)
                {
                    return Result<ImageResult>.Success(await SaveToPNG(buffer));
                }
                
                return await Crop.OnGravatarWasCropped(context.Object, buffer, h, w);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result<ImageResult>.Failed("图片尺寸太小。");
            }

            
        }
        
        private static async Task<ImageResult> SaveToPNG(byte[] buffer)
        {
            Configuration.Default.MemoryAllocator.ReleaseRetainedResources();

            var img = Image.Load<Rgba32>(buffer);

            img.Configuration
               .MemoryAllocator
               .ReleaseRetainedResources();

            var ms   = new MemoryStream(buffer.Length);
            var size = Math.Min(256, img.Width);

            img.Mutate(d => d.Resize(size, size));
            await img.SaveAsPngAsync(ms);
            img.Dispose();

            var array = ms.ToArray();
            ms.Seek(0, SeekOrigin.Begin);

            return new ImageResult
            {
                Id           = Guid.NewGuid().ToString("N"),
                Buffer       = array,
                OriginHeight = img.Height,
                OriginWidth  = img.Width,
                Width        = size,
                Height       = size,
            };
        }
        
        public static IViewBundleStateProvider UseBuiltinViews()
        {
            return new GravatarSystemBundle();
        }
        
        sealed class GravatarSystemBundle : IViewBundleStateProvider
        {

            public IEnumerable<ViewBundleState> Provide() => new []
            {
                new ViewBundleState(typeof(ImageEditorView), typeof(ImageEditorViewModel)),
            };
        }
    }
}