// ----------------------------------------------------------
//            文件：InheritedFeature.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 14:46
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database.AppModels.Features
{
    public sealed class InheritedFeature : Nameable, ISortable<InheritedFeature>
    {
        private string _image;
        private string _featureID;
        private string _options;

        private int _x;
        private int _y;

        public int CompareTo(InheritedFeature other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (other is null)
            {
                return 1;
            }

            return Index.CompareTo(other.Index);
        }


        public int Y
        {
            get => _y;
            set => SetValue(ref _y, value);
        }

        public int X
        {
            get => _x;
            set => SetValue(ref _x, value);
        }

        public int ImageHeight { get; set; }
        public int ImageWidth  { get; set; }

        public int Index { get; set; }

        public string Options
        {
            get => _options;
            set => SetValue(ref _options, value);
        }

        public string FeatureID
        {
            get => _featureID;
            set => SetValue(ref _featureID, value);
        }

        public string Image
        {
            get => _image;
            set => SetValue(ref _image, value);
        }
    }
}