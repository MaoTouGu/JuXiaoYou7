// ----------------------------------------------------------
//            文件：Save.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 13:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    partial class DesignViewModel
    {
        void DoSaveCommand()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                var r = Interop.SaveFileAsync("模板文件|*.template", "template", _template.Name);

                if (!r.IsFinished)
                {
                    return;
                }
                
                FileName = r.Value;
            }
            
            JSON2.ToFile(FileName, _template);
        }
        
        void DoSaveAsCommand()
        {
            var r = Interop.SaveFileAsync("模板文件|*.template", "template", _template.Name);

            if (!r.IsFinished)
            {
                return;
            }

            
            JSON2.ToFile(r.Value, _template);
        }

        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => SetValue(ref _fileName, value);
        }
    }
}