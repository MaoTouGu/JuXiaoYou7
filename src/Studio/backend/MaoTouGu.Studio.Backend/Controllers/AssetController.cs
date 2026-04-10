// ----------------------------------------------------------
//            文件：AssetController.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 13:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using System.Diagnostics;
using MaoTouGu.Foundation;
using Microsoft.AspNetCore.Authorization;

namespace MaoTouGu.Studio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController(IDatabaseService _Env) : Controller
    {
        private const int MaxSize_Emoji    = 8   * 1048576; // 表情包最大8MB；
        private const int MaxSize_Image    = 16  * 1048576; // 图片最大16MB；
        private const int MaxSize_Icon     = 1   * 1048576; // 图标最大为1MB；
        private const int MaxSize_Gravatar = 2   * 1048576; // 头像最大为2MB；
        private const int MaxSize_File     = 128 * 1048576; // 头像最大为128MB；


        private async Task<IActionResult> DoUpload(IFormFile file, string dir, int limit)
        {

            if (file is null || file.Length == 0)
            {
                return BadRequest("没有文件上传，取消请求.");
            }

            if (string.IsNullOrWhiteSpace(file.FileName) || !Guid.TryParse(file.FileName, out _))
            {
                return BadRequest("文件路径为空，上传失败。");
            }

            if (file.Length > limit)
            {
                return BadRequest($"超过支持上传的文件最大尺寸（最大支持{limit / 1048576}MB），不允许上传。");
            }

            // 目标路径
            var savePath = Path.Combine(dir, file.FileName);

            // 流式写入，不占内存
            await using var fs = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(fs);

            await fs.FlushAsync();
            Debug.WriteLine($"写入 ={savePath}");
            return Ok(new { FileName = file });
        }
        
        private IActionResult DoDownload(string dir, string fileName)
        {
            var filePath = Path.Combine(dir, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest("无法找到文件...");
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return File(stream, "image/jpeg", fileName);
        }
        
        #region UploadAttribute

        
        [Authorize]
        [HttpPost("upload_icon")]
        public Task<IActionResult> UploadIcon(IFormFile file) => DoUpload(file, IconDir, MaxSize_Icon);

        [Authorize]
        [HttpPost("upload_image")]
        public Task<IActionResult> UploadImage(IFormFile file) => DoUpload(file, ImageDir, MaxSize_Image);

        [Authorize]
        [HttpPost("upload_gravatar")]
        public Task<IActionResult> UploadGravatar(IFormFile file) => DoUpload(file, GravatarDir, MaxSize_Gravatar);

        [Authorize]
        [HttpPost("upload_file")]
        public Task<IActionResult> UploadFile(IFormFile file) => DoUpload(file, FileDir, MaxSize_File);

        [Authorize]
        [HttpPost("upload_emoji")]
        public Task<IActionResult> UploadEmoji(IFormFile file) => DoUpload(file, EmojiDir, MaxSize_Emoji);

        #endregion

        #region Download

        
        [Authorize]
        [HttpGet("download_emoji")]
        public IActionResult DownloadEmoji([FromQuery]string fileName) => DoDownload(EmojiDir, fileName);
        
        [Authorize]
        [HttpGet("download_image")]
        public IActionResult DownloadImage([FromQuery]string fileName) => DoDownload(ImageDir, fileName);
        
        [Authorize]
        [HttpGet("download_icon")]
        public IActionResult DownloadIcon([FromQuery]string fileName) => DoDownload(IconDir, fileName);
        
        [Authorize]
        [HttpGet("download_gravatar")]
        public IActionResult DownloadGravatar([FromQuery]string fileName) => DoDownload(GravatarDir, fileName);
        
        [Authorize]
        [HttpGet("download_file")]
        public IActionResult DownloadFile([FromQuery]string fileName) => DoDownload(FileDir, fileName);


        #endregion

        private string EmojiDir    => _Env.EmojiDir;
        private string FileDir     => _Env.FilesDir;
        private string GravatarDir => _Env.ImagesDir;
        private string ImageDir    => _Env.ImagesDir;
        private string IconDir     => _Env.ImagesDir;
    }
}