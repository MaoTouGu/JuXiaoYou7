// ----------------------------------------------------------
//            文件：OpenLinkCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 10:46
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Core
{
    public class OpenLinkCommand : _Command
    {
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(parameter?.ToString()) || !string.IsNullOrEmpty(Url);
        }

        public override void Execute(object parameter)
        {
            ProcessStartInfo info;

            if (parameter is not null)
            {
                info = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName        = parameter.ToString(),
                };
            }
            else
            {
                info = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName        = Url,
                    Arguments       = Arguments,
                };
            }

            Process.Start(info);
        }

        public string Arguments { get; init; }
        public string Url       { get; init; }
    }
}