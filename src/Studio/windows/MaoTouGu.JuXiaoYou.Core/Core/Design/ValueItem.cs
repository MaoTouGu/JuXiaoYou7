// ----------------------------------------------------------
//            文件：ValueItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月26日 14:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core.Design
{
    public class ValueItem : NameItem
    {
        private string _value;

        public string Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
    }
}