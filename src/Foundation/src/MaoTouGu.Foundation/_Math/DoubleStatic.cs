using System.Drawing;
using System.Runtime.InteropServices;

namespace MaoTouGu.Foundation.Mathematics
{
    public class DoubleStatic
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct NanUnion
        {
            [FieldOffset(0)]
            internal double DoubleValue;

            [FieldOffset(0)]
            internal ulong UintValue;
        }

        internal const double DBL_EPSILON = 2.2204460492503131E-16;

        internal const float FLT_MIN = 1.17549435E-38f;

        public static double GetAvailableValue(double value, bool allowNegative = false, bool allowPositive = false)
        {
            if (double.IsNaN(value))
            {
                return 0d;
            }

            if (!allowNegative && double.IsNegativeInfinity(value))
            {
                return 0d;
            }          
            
            if (!allowPositive && double.IsPositiveInfinity(value))
            {
                return 0d;
            }

            return value;
        }

        public static bool AreClose(double value1, double value2)
        {
            if (value1 == value2)
            {
                return true;
            }
            var num  = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.2204460492503131E-16;
            var num2 = value1 - value2;
            if (0.0 - num < num2)
            {
                return num > num2;
            }
            return false;
        }

        public static bool LessThan(double value1, double value2)
        {
            if (value1 < value2)
            {
                return !AreClose(value1, value2);
            }
            return false;
        }

        public static bool GreaterThan(double value1, double value2)
        {
            if (value1 > value2)
            {
                return !AreClose(value1, value2);
            }
            return false;
        }

        public static bool LessThanOrClose(double value1, double value2)
        {
            if (!(value1 < value2))
            {
                return AreClose(value1, value2);
            }
            return true;
        }

        public static bool GreaterThanOrClose(double value1, double value2)
        {
            if (!(value1 > value2))
            {
                return AreClose(value1, value2);
            }
            return true;
        }

        public static bool IsOne(double value)
        {
            return Math.Abs(value - 1.0) < 2.2204460492503131E-15;
        }

        public static bool IsZero(double value)
        {
            return Math.Abs(value) < 2.2204460492503131E-15;
        }

        public static bool AreClose(Size size1, Size size2)
        {
            if (AreClose(size1.Width, size2.Width))
            {
                return AreClose(size1.Height, size2.Height);
            }
            return false;
        }

        public static int DoubleToInt(double val)
        {
            if (!(0.0 < val))
            {
                return (int)(val - 0.5);
            }
            return (int)(val + 0.5);
        }

        public static bool IsNaN(double value)
        {
            var nanUnion = default(NanUnion);
            nanUnion.DoubleValue = value;
            var num  = nanUnion.UintValue & 0xFFF0000000000000uL;
            var num2 = nanUnion.UintValue & 0xFFFFFFFFFFFFFuL;
            if (num == 9218868437227405312L || num == 18442240474082181120uL)
            {
                return num2 != 0;
            }
            return false;
        }
    }
}