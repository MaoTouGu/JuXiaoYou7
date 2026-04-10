// ----------------------------------------------------------
//            文件：NameItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月26日 14:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core.Design
{
    public class NameItem : ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}