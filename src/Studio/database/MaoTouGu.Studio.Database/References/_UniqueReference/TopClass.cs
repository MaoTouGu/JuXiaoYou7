// ----------------------------------------------------------
//            文件：TopClass.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    public sealed class TopClass : ClassBase, IEquatable<TopClass>
    {
        private string _image;
        private int    _x;
        private int    _y;


        public bool Equals(TopClass other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Image       == other.Image       &&
                   Name        == other.Name        &&
                   X           == other.X           &&
                   Y           == other.Y           &&
                   ImageHeight == other.ImageHeight &&
                   ImageWidth  == other.ImageWidth  &&
                   Height      == other.Height      &&
                   Width       == other.Width       &&
                   Index       == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((TopClass)obj);
        }

        public override int GetHashCode()
        {
            var x = HashCode.Combine(Name, Image);
            var y = HashCode.Combine(X, Y, Height, Width, Index);
            var z = HashCode.Combine(ImageHeight, ImageWidth, Index);

            return HashCode.Combine(x, y, z);
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
        public int Height      { get; set; }
        public int Width       { get; set; }


        public string Image
        {
            get => _image;
            set => SetValue(ref _image, value);
        }
        public static bool operator ==(TopClass left, TopClass right) => Equals(left, right);
        public static bool operator !=(TopClass left, TopClass right) => !Equals(left, right);
    }
}