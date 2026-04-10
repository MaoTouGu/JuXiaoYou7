using System.IO;
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class LocalApi
    {
        private const string Download       = "/api/asset/download_";
        private const string Download_File  = "Files";
        private const string Download_Image = "Images";



        Task<Result<Stream>> ReadAsStream(string url, string id)
        {
            try
            {
                var dir      = DirectoryExt.Combine(url, Url);
                var fileName = Path.Combine(dir, id);

                var stream = File.Open(fileName, FileMode.Open);
                return Task.FromResult(Result<Stream>.Success(stream));
            }
            catch(Exception e)
            {
                return Task.FromResult(Result<Stream>.Failed(e.Message));
            }

        }

        async Task<Result<byte[]>> ReadAsArray(string url, string id)
        {
            try
            {
                var dir      = DirectoryExt.Combine(url, Url);
                var fileName = Path.Combine(dir, id);
                var buffer   = await File.ReadAllBytesAsync(fileName);

                return Result<byte[]>.Success(buffer);
            }
            catch(Exception e)
            {
                return Result<byte[]>.Failed(e.Message);
            }
        }

        //-------------------------------------------------------------
        //
        //                        Download
        //
        //-------------------------------------------------------------

        public async Task<Result<Stream>> DownloadFileAsStream(string id)
        {
            return await ReadAsStream(Download_File, id);
        }

        public async Task<Result<byte[]>> DownloadFileAsByteArray(string id)
        {
            return await ReadAsArray(Download_File, id);
        }


        public async Task<Result<Stream>> DownloadImageAsStream(string id, string dir)
        {
            return await ReadAsStream($"{Download_Image}{dir}", id);
        }
        public async Task<Result<byte[]>> DownloadImageAsByteArray(string id, string dir)
        {
            return await ReadAsArray($"{Download_Image}{dir}", id);
        }
    }
}