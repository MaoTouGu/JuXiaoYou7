// ----------------------------------------------------------
//            文件：SettingRangeFilter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// 设定项范围筛选器。
    /// </summary>
    public class SettingRangeFilter : CustomFilter
    {
        private string _key;
        private int    _start;
        private int    _end;
        private bool   _isReverse;
        

        public override bool Equals(CustomFilter other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is not SettingRangeFilter b)
            {
                return false;
            }
            
            return _key == b._key && 
                   _start == b._start &&
                   _end == b._end &&
                   _isReverse == b._isReverse;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((SettingRangeFilter)obj);
        }
        
        public override int GetHashCode() => HashCode.Combine(_key, _start, _end, _isReverse);

        /// <summary>
        /// 反转。
        /// </summary>
        /// <remarks>
        /// <para>True的情况代表判断小于Start，大于End的场景</para>
        /// <para>False的情况代表判断≥Start，≤End的场景</para>
        /// </remarks>
        public bool IsReverse
        {
            get => _isReverse;
            set => SetValue(ref _isReverse, value);
        }

        public int End
        {
            get => _end;
            set => SetValue(ref _end, value);
        }

        public int Start
        {
            get => _start;
            set => SetValue(ref _start, value);
        }

        public string Key
        {
            get => _key;
            set => SetValue(ref _key, value);
        }
    }
}