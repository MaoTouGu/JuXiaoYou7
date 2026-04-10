// ----------------------------------------------------------
//            文件：SettingEQFilter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:52
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// 设定项全等筛选器。例如设定中包含Name设定项时，筛选。
    /// </summary>
    public sealed class SettingEQFilter : CustomFilter
    {
        private string _key;
        

        public override bool Equals(CustomFilter other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is not SettingEQFilter b)
            {
                return false;
            }
            
            return _key == b._key;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((SettingEQFilter)obj);
        }
        
        public override int GetHashCode() => HashCode.Combine(_key);


        public string Key
        {
            get => _key;
            set => SetValue(ref _key, value);
        }
    }
}