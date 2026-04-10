// ----------------------------------------------------------
//            文件：SettingLTEFilter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// ≤范围筛选器
    /// </summary>
    public class SettingLTEFilter : CustomFilter
    {
        private string _key;
        private int    _value;


        public override bool Equals(CustomFilter other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is not SettingLTEFilter b)
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
            return Equals((SettingLTEFilter)obj);
        }
        
        public override int GetHashCode() => HashCode.Combine(_key, _value);
        public int Value
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