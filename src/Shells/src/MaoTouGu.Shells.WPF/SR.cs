

namespace MaoTouGu.Shells
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

        public static string Desc_StoryPath            => I18N.GetText("desc.StoryPath");
        public static string Desc_ClosePage            => I18N.GetText("desc.ClosePage");
        public static string Desc_NameCannotBeNull     => I18N.GetText("desc.NameCannotBeNull");
        public static string Desc_NameCannotDuplicated => I18N.GetText("desc.NameCannotDuplicated");
        public static string Desc_SaveSuccessful       => I18N.GetText("desc.SaveSuccessful");

        public static string Image_ScaleOrEdit       => I18N.GetText("txt.Image.ScaleOrEdit");
        public static string Image_ScaleOrEdit_Edit  => I18N.GetText("txt.Image.ScaleOrEdit.Edit");
        public static string Image_ScaleOrEdit_Scale => I18N.GetText("txt.Image.ScaleOrEdit.Scale");
        public static string Image_IconRequireSquare => I18N.GetText("txt.Image.Icon.RequireSquare");
        public static string Image_IconRequireSmall  => I18N.GetText("txt.Image.Icon.RequireSmall");
        public static string Image_NotSupportFormat  => I18N.GetText("txt.Image.NotSupportFormat");


        public static string Text_EmptyState  => I18N.GetText("txt.EmptyState");
        public static string Text_Named       => I18N.GetText("txt.Named");
        public static string Text_Unnamed     => I18N.GetText("txt.EmptyState");
        public static string Text_Deleted     => I18N.GetText("txt.Deleted");
        public static string Text_Removing    => I18N.GetText("txt.Removing");
        public static string Title_Unselected => I18N.GetText("txt.Unselected");
        
    }
}