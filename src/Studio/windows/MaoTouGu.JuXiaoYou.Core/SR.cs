// ----------------------------------------------------------
//            文件：SR.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月16日 16:02
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou
{
    public static class SR
    {
        
        public const string Image_All = "支持的图片文件|*.png;*.jpg;*.jpeg;*.bmp";
        public const string Image_Png = "PNG图片文件|*.png";
        
        
        public static string Title_Success    => I18N.GetText("txt.Title.Success");
        public static string Title_Info       => I18N.GetText("txt.Title.Info");
        public static string Title_Danger     => I18N.GetText("txt.Title.Danger");
        public static string Title_Warning    => I18N.GetText("txt.Title.Warning");
        public static string Title_Processing => I18N.GetText("txt.Title.Processing");
        public static string Title_New        => I18N.GetText("txt.Title.New");
        public static string Title_Edit       => I18N.GetText("txt.Title.Edit");
        public static string Title_Rename     => I18N.GetText("txt.Title.Rename");
        public static string Title_Remove     => I18N.GetText("txt.Title.Remove");
        public static string Title_SaveFile   => I18N.GetText("txt.Title.SaveFile");
    }
}