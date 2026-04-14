// ----------------------------------------------------------
//            文件：Save.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 13:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Pages
{
    partial class DesignViewModel
    {
        private readonly TemplateProject _templateProject;

        private string _fileName;

        void DoSaveCommand()
        {
            if (_templateProject is not null)
            {
                _fileName             = _templateProject.FileName;
                _templateProject.Name = _template.Name;
            }

            if (string.IsNullOrEmpty(_fileName))
            {
                var r = Interop.SaveFileAsync(ExtFilters.TypographyTemplate, ExtFilters.TypographyTemplateExt, _template.Name);

                if (!r.IsFinished)
                {
                    return;
                }

                _fileName = r.Value;

                if (_templateProject is not null)
                {
                    _templateProject.FileName = r.Value;
                    GlobalSettings.Save();
                }
            }

            JSON2.ToFile(_fileName, _template);

        }

        void DoSaveAsCommand()
        {
            var r = Interop.SaveFileAsync(ExtFilters.TypographyTemplate, ExtFilters.TypographyTemplateExt, _template.Name);

            if (!r.IsFinished)
            {
                return;
            }


            JSON2.ToFile(r.Value, _template);
        }
    }
}