namespace KinonekoSoftware.UI.Charts
{
    public partial class StackedProgressControl : ChartControl
    {
        private const double RevisionFactor = 0.01d;
        public class RenderData
        {
            public double   Width   { get; internal set; }
            public double   Height  { get; internal set; }
            public double[] Sizes   { get; internal set; }
        }

        private RenderData _data;


        protected void Prepare()
        {
            var values = Values ?? new ChartSeries();

            if (values.Count == 0)
            {
                return;
            }

            GetSize(out var w, out var h);

            var remain     = values.Count;
            var revision   = 0d;
            var max        = values.Sum(x => x.Value);
            var isVertical = IsVertical;

            _data = new RenderData
            {
                Width  = w,
                Height = h,
                Sizes  = new double[remain],
            };

            
            //
            // 在过去渲染的时候，小于1%的部分都会以真实的大小显示
            // 这会导致该部分的实际视觉效果非常的微弱，所以我们决定使用clamp来修正
            // 所有小于1%的Series都会算成1%，并把1% - percent的实际值累加起来，存储于revision当中，
            // revision这部分的值将会被所有大于1%的部分分摊。
            foreach (var series in values)
            {
                if (series is null)
                {
                    remain -= 1;
                    continue;
                }

                var v       = Math.Clamp(series.Value, 0, max);
                var percent = v / max;


                if (DoubleStatic.LessThan(percent, RevisionFactor))
                {
                    remain   -= 1;
                    revision += (RevisionFactor - percent);
                }
            }

            //
            // 均摊
            revision /= remain;
            
            for (var i = 0; i < values.Count; i++)
            {
                var series = values[i];

                if (series is null)
                {
                    continue;
                }

                var v       = Math.Clamp(series.Value, 0, max);
                var percent = Math.Clamp(v / max, RevisionFactor, 1d);

                if (DoubleStatic.GreaterThan(percent, RevisionFactor))
                {
                    //
                    // 修正
                    percent -= revision;
                }
                
                _data.Sizes[i] = percent * (isVertical ? h : w);
            }

        }


        private void Draw(DrawingContext dc)
        {
            if (_data is null) return;

            var palette = Palette    ?? ChartPalette.CreateRainbow();
            var bg      = Background ?? new SolidColorBrush(Colors.Gray);

            //
            //
            dc.DrawRectangle(bg, null, new Rect(0, 0, _data.Width, _data.Height));

            //
            //
            // dc.DrawEllipse(null, bgPen, _data.Center, _data.Radius, _data.Radius);

            var lastSize = 0d;

            if (IsVertical)
            {
                for (var i = 0; i < _data.Sizes.Length; i++)
                {
                    var value = _data.Sizes[i];

                    if (DoubleStatic.AreClose(0, value))
                    {
                        continue;
                    }

                    var fg = GetBrush(i, palette);

                    if (DoubleStatic.AreClose(1, value))
                    {
                        dc.DrawRectangle(fg, null, new Rect(0, 0, _data.Width, _data.Height));
                    }
                    else
                    {
                        dc.DrawRectangle(fg, null, new Rect(0, lastSize, _data.Width, value));
                        lastSize += value;
                    }

                    //dc.DrawEllipse(brush, null, part.End, 2, 2);
                    //if (i == 0) dc.DrawEllipse(brush, null, part.Start, 2, 2);
                }
            }
            else
            {
                for (var i = 0; i < _data.Sizes.Length; i++)
                {
                    var value = _data.Sizes[i];

                    if (DoubleStatic.AreClose(0, value))
                    {
                        continue;
                    }

                    var fg = GetBrush(i, palette);

                    if (DoubleStatic.AreClose(1, value))
                    {
                        dc.DrawRectangle(fg, null, new Rect(0, 0, _data.Width, _data.Height));
                    }
                    else
                    {
                        dc.DrawRectangle(fg, null, new Rect(lastSize, 0, value, _data.Height));
                        lastSize += value;
                    }

                    //dc.DrawEllipse(brush, null, part.End, 2, 2);
                    //if (i == 0) dc.DrawEllipse(brush, null, part.Start, 2, 2);
                }
            }


            //
            //

            //
            // Debug
        }

        private Brush GetBrush(int i, ChartPalette palette)
        {
            if(Values.Count == 0)
            {
                return null;
            }

            var color = Values[i].Color;

            if (string.IsNullOrEmpty(color))
            {
                return palette.GetBrush(i);
            }
            
            return new SolidColorBrush(Xaml.ToColor(color));
        }

        /// <summary>
        /// 渲染数据
        /// </summary>
        public RenderData DrawingInfo => _data;
    }
}