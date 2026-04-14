// ----------------------------------------------------------
//            文件：Int32Thickness.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database.Topology
{
    public struct Int32Thickness
    {
        
        // 
        //
        //
        
        private int _left;
        private int _top;
        private int _right;
        private int _bottom;

        public int Bottom
        {
            get => _bottom;
            set => _bottom = value;
        }
        public int Right
        {
            get => _right;
            set => _right = value;
        }
        public int Top
        {
            get => _top;
            set => _top = value;
        }
        public int Left
        {
            get => _left;
            set => _left = value;
        }
    }
}