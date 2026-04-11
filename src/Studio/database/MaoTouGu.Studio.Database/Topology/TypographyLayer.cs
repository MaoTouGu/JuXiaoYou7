// ----------------------------------------------------------
//            文件：TypographyLayer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 15:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation.Collections;

namespace MaoTouGu.Studio.Database.Topology
{
    public sealed class TypographyLayer : Nameable
    {
        private bool _isLock;
        
        public List<string> Blocks { get; init; }

        public bool IsLock
        {
            get => _isLock;
            set => SetValue(ref _isLock, value);
        }
    }
}