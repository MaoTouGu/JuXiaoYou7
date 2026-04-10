using System.Drawing;
using MaoTouGu.Foundation;
using Color = Avalonia.Media.Color;

namespace KinonekoSoftware.UI.Charts
{
    public class ChartPalette : ObservableObject , IChartPalette
    {
        private readonly Brush[] _NonTransparentBrushes;
        private readonly Brush[] _TransparentBrushes;
        private          bool        _useMonoPalette;
        
        internal ChartPalette(string stroke, IEnumerable<string> brushes, bool createTransparent) : 
            this(Xaml.ToColor(stroke), brushes.Select(Xaml.ToColor), createTransparent)
        {
            
        }

        internal ChartPalette(Brush stroke, IEnumerable<Brush> brushes)
        {
            _NonTransparentBrushes = (brushes ?? Array.Empty<Brush>()).ToArray();
            Stroke                 = stroke;
        }

        internal ChartPalette(Brush stroke, IEnumerable<Brush> nTBrushes, IEnumerable<Brush> tBrushes)
        {
            _NonTransparentBrushes = (nTBrushes ?? Array.Empty<Brush>()).ToArray();
            _TransparentBrushes    = (tBrushes  ?? Array.Empty<Brush>()).ToArray();
            Stroke                 = stroke;
        }

        internal ChartPalette(Color stroke, IEnumerable<Color> colors, bool createTransparent)
        {
            var c = (colors ?? new Color[] { Color.FromRgb(0x00, 0x7a, 0xcc) }).ToArray();

            Stroke                 = new SolidColorBrush(stroke);
            _NonTransparentBrushes = c.Select(GetNonTransparentBrush).ToArray();
            _TransparentBrushes    = (createTransparent ? c.Select(GetTransparentBrush) : c.Select(GetNonTransparentBrush)).ToArray();
        }

        private static Brush GetTransparentBrush(Color c) => new SolidColorBrush
        {
            Color   = c,
            Opacity = 0.25d,
        };

        private static Brush GetNonTransparentBrush(Color c) => new SolidColorBrush
        {
            Color   = c,
            Opacity = 1d,
        };


        
        
        #region CreateColor
        
        public static ChartPalette Create(string stroke, string color, bool transparency = true)
        {
            return new ChartPalette(stroke, new[] { color }, transparency);
        }

        internal static ChartPalette Create(Color stroke, Color color, bool transparency = true)
        {
            return new ChartPalette(stroke, new[] { color }, transparency);
        }

        public static ChartPalette Create(Color stroke, Color c1, Color c2, bool transparency = true)
        {
            return new ChartPalette(stroke, new[] { c1, c2 }, transparency);
        }

        public static ChartPalette Create(Color stroke, Color c1, Color c2, Color c3, bool transparency = true)
        {
            return new ChartPalette(stroke, new[] { c1, c2, c3 }, transparency);
        }

        public static ChartPalette Create(Color stroke, Color c1, Color c2, Color c3, Color c4, bool transparency = true)
        {
            return new ChartPalette(stroke, new[] { c1, c2, c3, c4 }, transparency);
        }

        public static ChartPalette Create(Color stroke, IEnumerable<Color> c, bool transparency = true)
        {
            return new ChartPalette(stroke, c, transparency);
        }

        #endregion

        #region CreateRainbow

        public static ChartPalette CreateRainbow()
        {
            return new ChartPalette(Colors.Gray, new[]
            {
                Color.FromRgb(0xA9, 0x36, 0x36),
                Color.FromRgb(0xA9, 0x70, 0x36),
                Color.FromRgb(0xA9, 0xA9, 0x36),
                Color.FromRgb(0x36, 0xA9, 0xA9),
                Color.FromRgb(0x70, 0xA9, 0x36),
                Color.FromRgb(0x36, 0x70, 0xA9),
                Color.FromRgb(0x36, 0xA9, 0x36),
                Color.FromRgb(0x36, 0x36, 0xA9),
                Color.FromRgb(0x36, 0xA9, 0x70),
                Color.FromRgb(0x70, 0x36, 0xA9),
                Color.FromRgb(0x82, 0x36, 0xA9),
                Color.FromRgb(0xA9, 0x36, 0xA9),
                Color.FromRgb(0xA9, 0x36, 0x70),
                // Color.FromRgb(0xA9, 0x36, 0x70),
                // Color.FromRgb(0xA9, 0x36, 0x36),
                // Color.FromRgb(0xA9, 0x5C, 0x36),
                // Color.FromRgb(0xA9, 0x82, 0x36),
                // Color.FromRgb(0xA9, 0xA9, 0x36),
                // Color.FromRgb(0x82, 0xA9, 0x36),
                // Color.FromRgb(0x5C, 0xA9, 0x36),
                // Color.FromRgb(0x36, 0xA9, 0x36),
                // Color.FromRgb(0x36, 0xA9, 0x5C),
                // Color.FromRgb(0x36, 0xA9, 0x82),
                // Color.FromRgb(0x36, 0xA9, 0xA9),
                // Color.FromRgb(0x36, 0x82, 0xA9),
                // Color.FromRgb(0x36, 0x5C, 0xA9),
                // Color.FromRgb(0x36, 0x36, 0xA9),
                // Color.FromRgb(0x5C, 0x36, 0xA9),
                // Color.FromRgb(0x82, 0x36, 0xA9),
                // Color.FromRgb(0xA9, 0x36, 0xA9),
                // Color.FromRgb(0xA9, 0x36, 0x82),
                // Color.FromRgb(0xA9, 0x36, 0x5C),
            }, true);
        }

        public static ChartPalette RainBow { get; } = CreateRainbow();

        #endregion

        /// <summary>
        /// 获取指定索引值的颜色。
        /// </summary>
        /// <param name="index">指定的索引号</param>
        /// <param name="transparency">是否获得半透明颜色</param>
        public Brush GetBrush(int index, bool transparency = false)
        {
            if (_useMonoPalette)
            {
                return transparency ? _TransparentBrushes[0] : _NonTransparentBrushes[0];
            }


            var i = index % _NonTransparentBrushes.Length;
            return transparency ? _TransparentBrushes[i] : _NonTransparentBrushes[i];
        }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<Brush> NonTransparentBrushes => _NonTransparentBrushes;

        /// <summary>
        /// 
        /// </summary>
        public Brush Stroke { get; init; }


        /// <summary>
        /// 是否使用单色调色板（即便当前的调色板有多个颜色可供选择。）
        /// </summary>
        public bool UseMonoPalette
        {
            get => _useMonoPalette;
            set => SetValue(ref _useMonoPalette, value);
        }
    }
}