// ----------------------------------------------------------
//            文件：CountToVisibilityConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 01:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Converters
{
    public sealed class ZeroToVisibilityConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value switch
            {
                sbyte ui8   => ui8,
                byte i8     => i8,
                short i16   => i16,
                ushort ui16 => ui16,
                int i32     => i32,
                uint ui32   => (int)ui32,
                long i64    => (int)i64,
                ulong ui64  => (int)ui64,
                float f32   => (int)f32,
                double f64  => (int)f64,
                _           => 0,
            };

            return v == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    } 
    
    public sealed class NotZeroToVisibilityConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value switch
            {
                sbyte ui8   => ui8,
                byte i8     => i8,
                short i16   => i16,
                ushort ui16 => ui16,
                int i32     => i32,
                uint ui32   => (int)ui32,
                long i64    => (int)i64,
                ulong ui64  => (int)ui64,
                float f32   => (int)f32,
                double f64  => (int)f64,
                _           => 0,
            };

            return v != 0 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}