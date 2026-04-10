using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class RemoteApi
    {
        private const string Upload_Emoji    = "/api/Asset/upload_emoji";
        private const string Upload_File     = "/api/Asset/upload_file";
        private const string Upload_Image    = "/api/Asset/upload_image";
        private const string Upload_Gravatar = "/api/Asset/upload_gravatar";
        private const string Upload_Icon     = "/api/Asset/upload_icon";

        //-------------------------------------------------------------
        //
        //                         UploadAttribute 
        //
        //-------------------------------------------------------------

        async Task<Result> Upload(string url, string id, Stream stream)
        {
            // try
            // {
            //     var streamContent = new StreamContent(stream, 4096);
            //     var response      = await Client.PostAsync($"{url}?fileName={id}", streamContent);
            //
            //     if (!response.IsSuccessStatusCode)
            //     {
            //         var reason = await response.Content.ReadAsStringAsync();
            //         return Result.Failed(reason);
            //     }
            //
            //     return Result.Success();
            // }
            // catch(Exception e)
            // {
            //     Console.WriteLine(e);
            //     return Result.Failed(e.Message);
            // }

            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    await using (stream)
                    {
                        var fileContent = new StreamContent(stream);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                        // 添加文件部分，字段名可根据服务端要求修改（如 "file"）
                        formData.Add(fileContent, "file", id);

                        // 可选：添加其他表单字段
                        // formData.Add(new StringContent("some value"), "fieldName");

                        // 发送请求（可添加进度报告，但需要自定义 ProgressableStreamContent，此处简化）
                        var response = await Client.PostAsync(url, formData);

                        if (response.IsSuccessStatusCode)
                        { 
                            return Result.Success();
                        }
                        
                        return Result.Failed($"上传失败：{response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    }
                }
            }
            catch(Exception e)
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