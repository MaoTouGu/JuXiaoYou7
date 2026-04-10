// ----------------------------------------------------------
//            文件：ImageEditorCallback.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 15:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public delegate Task<Result<Tuple<int, int, int, int>>> ImageEditorCallback(ImageEditorViewModel context);
}