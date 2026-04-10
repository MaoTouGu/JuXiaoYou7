// ----------------------------------------------------------
//            文件：IImageDownloadService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 16:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public interface IImageDownloadService
    {
        Task<Result<Stream>> DownloadImageAsStream(string id, string dir);

        Task<Result<byte[]>> DownloadImageAsByteArray(string id, string dir);
    }
}