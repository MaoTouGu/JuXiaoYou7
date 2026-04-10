

using Image = SixLabors.ImageSharp.Image;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static class ImageInfo
    {
        public static bool GetMetadata(ReadOnlySpan<byte> buffer, out int width, out int height)
        {
            try
            {
                var info          = Image.Identify(buffer);
                var metadata      = info.Metadata;
                var decodedFormat = metadata.DecodedImageFormat;
                var fileExt       = decodedFormat?.FileExtensions;


                if (decodedFormat is not null &&
                    fileExt is not null       &&
                    fileExt.Any(x => x is "gif"))
                {
                    width  = info.Width;
                    height = info.Height;
                    return false;
                }

                width  = info.Width;
                height = info.Height;
                return true;
            }
            catch
            {
                width  = -1;
                height = -1;
                return false;
            }
        }

        public static bool GetImageInfo(ReadOnlySpan<byte> buffer, out int width, out int height) => GetMetadata(buffer, out width, out height);
    }
}