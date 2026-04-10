using System.IO;
using System.Net;
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class RemoteApi
    {
        private const string Download      = "/api/asset/download_";
        private const string Download_File = "/api/asset/download_file";



        async Task<Result<Stream>> ReadAsStream(string url, string id)
        {
            try
            {
                var response = await Client.GetAsync($"{url}?fileName={id}");

                if (response is null)
                {
                    //
                    // 非常严重的错误，但是没有预期
                }

                if (!response.IsSuccessStatusCode)
                {
                    //
                    //
                    return Result<Stream>.Failed("请求错误");
                }

                var stream = await response.Content.ReadAsStreamAsync();
                return Result<Stream>.Success(stream);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<Stream>.Failed(e.Message);
            }

        }
        
        /// <summary>
        /// 下载图片调用此方法。与<see cref="ReadAsStream"/>相比多了一个图片大小的判断。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        async Task<Result<Stream>> ReadAsStreamCritical(string url, string id)
        {
            try
            {
                var response = await Client.GetAsync($"{url}?fileName={id}");

                if (response is null)
                {
                    //
                    // 非常严重的错误，但是没有预期
                }

                if (!response.IsSuccessStatusCode)
                {
                    //
                    //
                    return Result<Stream>.Failed("请求错误");
                }
                
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.Content.Headers.ContentLength is {} length && length < 16 * 1048576)
                {
                    return Result<Stream>.Success(stream);
                }
                
                return Result<Stream>.Failed("请求错误，返回的文件过大");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<Stream>.Failed(e.Message);
            }

        }
        
        async Task<Result<byte[]>> ReadAsArrayCritical(string url, string id)
        {
            try
            {
                var response = await Client.GetAsync($"{url}?fileName={id}");

                if (response is null)
                {
                    //
                    // 非常严重的错误，但是没有预期
                }

                if (!response.IsSuccessStatusCode)
                {
                    //
                    //
                    return Result<byte[]>.Failed("请求错误");
                }
                
                if (response.Content
                            .Headers
                            .ContentLength is {} length && length < 16 * 1048576)
                {
                    var stream = await response.Content.ReadAsByteArrayAsync();
                    return Result<byte[]>.Success(stream);
                }

                
                return Result<byte[]>.Failed("请求错误，返回的文件过大");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Result<byte[]>.Failed(e.Message);
            }

        }

        async Task<Result<byte[]>> ReadAsArray(string url, string id)
        {
            try
            {
                var response = await Client.GetAsync($"{url}?fileName={id}");

                if (response is null)
                {
                    //
                    // 非常严重的错误，但是没有预期
                }

                if (!response.IsSuccessStatusCode)
                {
                    //
                    //
                    return Result<byte[]>.Failed("请求错误");
                }

                var stream = await response.Content.ReadAsByteArrayAsync();
                return Result<byte[]>.Success(stream);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
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
            return await ReadAsStreamCritical($"{Download}{dir}", id);
        }
        public async Task<Result<byte[]>> DownloadImageAsByteArray(string id, string dir)
        {
            return await ReadAsArrayCritical($"{Download}{dir}", id);
        }
    }
}