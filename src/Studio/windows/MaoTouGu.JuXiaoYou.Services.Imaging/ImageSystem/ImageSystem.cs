using System.Diagnostics;
using System.Windows;
using MaoTouGu.Foundation.Collections;
using MaoTouGu.JuXiaoYou.Services.Imaging.Caching;
using Microsoft.Xaml.Behaviors;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static partial class ImageSystem
    {


        public static async Task<ImageSource> Get(string id, ImageType type, ImageFallbackOption fallback, ThumbnailLevel thumbnail)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return GetFallbackImage(fallback);
                }

                var r = LRU(id, thumbnail);

                //
                // 判断缓存中是否存在指定的图片。
                if (r is not null)
                {
                    return r;
                }

                //
                // 直接创建Bitmap
                var image = await Load(id, thumbnail, fallback, GetDir(type));

                if (image is not null)
                {
                    return image;
                }

                return GetFallbackImage(fallback);
            }
            catch
            {
                return GetFallbackImage(fallback);
            }
        }

        public static string GetDir(ImageType type) => type switch
        {
            ImageType.Image    => nameof(ImageType.Image),
            ImageType.Gravatar => nameof(ImageType.Gravatar),
            ImageType.Icon     => nameof(ImageType.Icon),
            ImageType.Emoji    => nameof(ImageType.Emoji),
            _                  => nameof(ImageType.Other),
        };

        public static async Task<BitmapImage> Load(string gravatar, ThumbnailLevel thumbnail, ImageFallbackOption fallback, string dir)
        {
            //
            // 判断是否为本地图片
            if (File.Exists(gravatar))
            {
                return await ImageCacheService.RequireLocal(gravatar, gravatar, thumbnail);
            }
            
            var path = DirectoryExt.Combine(ImageSystem.RootPath, dir);
            
            //
            // 判断是否为本地图片。
            if (string.IsNullOrEmpty(path))
            {
                return ImageSystem.GetFallbackImage(fallback);
            }

            //
            // 文件路径
            var fileName = Path.Combine(path, gravatar);

            //
            // 判断是否存在，不存在则尝试在网络上加载。
            if (!File.Exists(fileName))
            {
                await ImageCacheService.RequireDownload(gravatar, dir, fileName);
            }

            return await ImageCacheService.RequireLocal(gravatar, fileName, thumbnail);
        }

        public static BitmapImage LRU(string gravatar, ThumbnailLevel thumbnail)
        {
            var table = thumbnail switch
            {
                ThumbnailLevel.Of360p  => ImageCacheService._360pThumb,
                ThumbnailLevel.Of720p  => ImageCacheService._720pThumb,
                ThumbnailLevel.Of1080p => ImageCacheService._1080pThumb,
                _                      => ImageCacheService._NoneThumb,
            };

            if (table.TryGetValue(gravatar, out var maybeGravatar))
            {
                if (maybeGravatar.TryGetTarget(out var image))
                {
                    return image;
                }

                table.TryRemove(gravatar, out _);
                Debug.WriteLine("LRU -> Release Rubbish");
            }

            return null;
        }

        public static BitmapImage GetFallbackImage(ImageFallbackOption fallback)
        {
            return fallback switch
            {
                ImageFallbackOption.Gravatar => Gravatar,
                ImageFallbackOption.Icon     => Icon,
                ImageFallbackOption.Image    => Image,
                _                            => null,
            };
        }


    }
}