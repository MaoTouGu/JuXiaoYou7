// ----------------------------------------------------------
//            文件：TypographyPage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 23:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation.Collections;

namespace MaoTouGu.Studio.Database.Topology
{
    public class TypographyPage : Nameable
    {
        private object _bitmap;
        private bool   _isDynamic;
        private bool   _isLock;
        private int    _height;

        public int Height
        {
            get => _height;
            set => SetValue(ref _height, value);
        }

        public bool IsLock
        {
            get => _isLock;
            set => SetValue(ref _isLock, value);
        }

        public bool IsDynamic
        {
            get => _isDynamic;
            set => SetValue(ref _isDynamic, value);
        }


        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public object Bitmap
        {
            get => _bitmap;
            set => SetValue(ref _bitmap, value);
        }

        public ViewList<TypographyBlock> Blocks { get; init; }
        public ViewList<TypographyLayer> Layers { get; init; }
    }
}