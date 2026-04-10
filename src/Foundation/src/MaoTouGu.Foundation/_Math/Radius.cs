namespace MaoTouGu.Foundation.Mathematics
{
    public sealed class Radius
    {
        private double _r;
        private double _centerX;
        private double _centerY;

        public Radius()
        {
            _r = 0d;
        }

        public Radius(double w, double h)
        {
            Track(w, h);
        }

        public void Track(double w, double h)
        {
            w        = IsCorrect(w) ? w : 0d;
            h        = IsCorrect(w) ? h : 0d;
            _r       = Math.Min(w, h) / 2d;
            _centerX = w              / 2d;
            _centerY = h              / 2d;
        }

        public Angle GetAngle(double x, double y)
        {
            x -= _centerX;
            y -= _centerY;
            return new Angle(Math.Atan2(y, x));
        }
        
        public Angle GetAngle(double x, double y, Angle angle)
        {
            x -= _centerX;
            y -= _centerY;
            angle.Change(Math.Atan2(y, x));
            return angle;
        }

        private static bool IsCorrect(double v) => !double.IsInfinity(v) && !double.IsNaN(v);

        public double Value => _r;

        public double CenterX => _centerX;
        public double CenterY => _centerY;
    }
}