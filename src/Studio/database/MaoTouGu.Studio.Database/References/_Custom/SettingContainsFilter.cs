// ----------------------------------------------------------
//            文件：SettingContainsFilter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// 设定项包含筛选器。例如设定中包含Na**设定项时，筛选。
    /// </summary>
    public class SettingContainsFilter : CustomFilter
    {
        private string _key;
        private string _value;

        public override bool Equals(CustomFilter other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is not SettingContainsFilter b)
            {
                return false;
            }
            
            return _key == b._key && _value == b._value;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((SettingContainsFilter)obj);
        }
        
        public override int GetHashCode() => HashCode.Combine(_key, _value);
        
        
        public string Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }

        public string Key
        {
            get => _key;
            set => SetValue(ref _key, value);
        }
    }
}