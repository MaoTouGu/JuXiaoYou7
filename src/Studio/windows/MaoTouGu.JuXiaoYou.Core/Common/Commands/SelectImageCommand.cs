// ----------------------------------------------------------
//            文件：SelectImageCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 23:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Common.Commands
{
    public abstract class SelectImageCommand<TItem, TContext>(TContext target) : ContextCommand<TItem, TContext>(target) where TItem : class
    {
        protected sealed override async void Execute(TItem target)
        {
            var r   = Interop.OpenFileAsync(SR.Image_All);
            var api = Ioc.SafeGet<IWebApi>();

            if (!r.IsFinished)
            {
                return;
            }


            try
            {
                var             buffer = await File.ReadAllBytesAsync(r.Value);
                await using var stream = new MemoryStream(buffer);

                var id = ID.Get();
                var r2 = await api.UploadImage(id, stream);

                if (!r2.IsFinished)
                {
                    return;
                }

                ImageInfo.GetImageInfo(buffer, out var w, out var h);

                //
                // 默认图片大小为 300dpi * 88 & 60
                OnSetImage(target, id, w, h);
            }
            catch(Exception exception)
            {
                OnSetImageFailed(target);
            }
        }

        protected abstract void OnSetImage(TItem item, string id, int w, int h);
        protected abstract void OnSetImageFailed(TItem item);
    }
}