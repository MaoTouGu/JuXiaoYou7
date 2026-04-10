using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class LocalApi
    {
        private const string Upload_Emoji    = "Emoji";
        private const string Upload_File     = "Files";
        private const string Upload_Image    = "Images";
        private const string Upload_Gravatar = "Gravatar";
        private const string Upload_Icon     = "Icon";

        //-------------------------------------------------------------
        //
        //                         UploadAttribute 
        //
        //-------------------------------------------------------------

        async Task<Result> Upload(string url, string id, Stream stream)
        {
            try
            {
                var dir      = DirectoryExt.Combine(url, Url);
                var fileName = Path.Combine(dir, id);

                await using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                await stream.CopyToAsync(fs);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failed(e.Message);
            }
            
        }

        public async Task<Result> UploadFile(string id, Stream stream)
        {
            return await Upload(Upload_File, id, stream);
        }

        public async Task<Result> UploadEmoji(string id, Stream stream)
        {
            return await Upload(Upload_Emoji, id, stream);
        }

        public async Task<Result> UploadImage(string id, Stream stream)
        {
            return await Upload(Upload_Image, id, stream);
        }

        public async Task<Result> UploadGravatar(string id, Stream stream)
        {
            return await Upload(Upload_Gravatar, id, stream);
        }

        public async Task<Result> UploadIcon(string id, Stream stream)
        {
            return await Upload(Upload_Icon, id, stream);
        }
    }
}