// ----------------------------------------------------------
//            文件：RadarVisualizer.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 18:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public partial class RadarPresenter : IVisualizerSettingWorker
    {
        private readonly ChartSeriesCollection      _chart;
        private readonly ChartSeries                _series;
        private readonly ChartAxisCollection        _axis;
        private readonly Dictionary<string, Series> _map;

        public RadarPresenter()
        {
            InitializeComponent();

            //
            //
            _chart  = new ChartSeriesCollection();
            _series = new ChartSeries();
            _axis   = new ChartAxisCollection();
            _map    = new();
            //
            //
            _chart.Add(_series);
        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        void SetPalette(RadarVisualizer rv)
        {
            RadarControl.Palette = ChartPalette.Create("#808080", rv.GetPalette());
        }

        void MaintainMaximum(RadarVisualizer rv)
        {
            foreach (var item in _series)
            {
                item.Value = Math.Clamp(item.Value, 0, rv.Maximum);
            }

            _chart.Minimum  = 0;
            _chart.Maximum  = rv.Maximum;
            _series.Minimum = 0;
            _series.Maximum = rv.Maximum;
        }

        static int GetNumeric(MonikerSettingSet setting, string key)
        {
            if (!setting.TryGetValue(key, out var rawValue))
            {
                setting.TryAdd(key, string.Empty);
                return 0;
            }

            return int.TryParse(rawValue, out var n) ? n : 0;
        }

        public void DoWork(Moniker moniker, IVisualizerOptions options, TypographyBlockVPO target, string name)
        {
            if (_map.TryGetValue(name, out var series))
            {
                series.Value = GetNumeric(moniker.Settings, name);
            }
        }

        protected override void Setup(Moniker m, IVisualizerOptions options)
        {
            if (options is not RadarVisualizer rv)
            {
                return;
            }

            StructureChangedOverride(m, options);
            OptionChangedOverride(m, options);

            //
            //
            RadarControl.Axes   = _axis;
            RadarControl.Values = _chart;

            //
            // 创建monitor

            SetMonitor();
        }

        protected override void OptionChangedOverride(Moniker m, IVisualizerOptions options)
        {
            if (options is not RadarVisualizer rv)
            {
                return;
            }

            SetPalette(rv);
            MaintainMaximum(rv);
        }

        protected override void StructureChangedOverride(Moniker m, IVisualizerOptions options)
        {
            if (options is not RadarVisualizer rv)
            {
                return;
            }

            //
            //
            _axis.Clear();
            _series.Clear();
            _map.Clear();
            //
            // Rebuild Collection
            foreach (var item in rv.Collection)
            {
                var axis   = new ChartAxis { Id = item.Id, Name = item.Name, Color = item.Color };
                var series = new Series { Id    = item.Id, };

                if (!string.IsNullOrEmpty(item.MetadataSource))
                {
                    series.Value = GetNumeric(m.Settings, item.MetadataSource);

                    if (_map.TryAdd(item.MetadataSource, series))
                    {
                    }
                }
                
                _axis.Add(axis);
                _series.Add(series);
            }
        }
    }
}