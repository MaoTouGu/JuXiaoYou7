// ----------------------------------------------------------
//            文件：GlobalObjectPickerView.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 02:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    public interface IGlobalObjectPicker
    {
        string PropertyName { get; }
    }
    
    public sealed class GlobalObjectPicker<T> : PickerRoot<T>, IGlobalObjectPicker
    {
        public GlobalObjectPicker(IEnumerable<T> collection, string propertyName) : base()
        {
            Collection.AddMany(collection, true);
            PropertyName = propertyName;
        }
        
        public string PropertyName { get; }
    }
}