// ----------------------------------------------------------
//            文件：KVItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月19日 14:02
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou
{
    public class KVItem : ObservableObject
    {
        private string _name;
        private string _value;

        /// <summary>
        /// 获取或设置 <see cref="Value"/> 属性。
        /// </summary>
        public string Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
        
        /// <summary>
        /// 获取或设置 <see cref="Name"/> 属性。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }

    public abstract class KVItem<T> : ObservableObject
    {
        private string _name;
        private T _value;

        /// <summary>
        /// 获取或设置 <see cref="Value"/> 属性。
        /// </summary>
        public T Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
        
        /// <summary>
        /// 获取或设置 <see cref="Name"/> 属性。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
    
    public sealed class Int32Value : KVItem<int>{}
    public sealed class BooleanValue : KVItem<bool>{}
}