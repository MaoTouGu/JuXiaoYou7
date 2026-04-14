// ----------------------------------------------------------
//            文件：RadarVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 16:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Visualizers.Blocks;

namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public class RadarVisualizer : CollectionVisualizerOptions<RadarVisualizerSettingView, RadarPresenter, RadarItemFrom>, IChartPaletteSource
    {
        private int    _maximum;
        private string _color;

        public RadarVisualizer()
        {
        }
        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        protected override RadarItemFrom CreateItem(string name) => new RadarItemFrom
        {
            Id   = ID.Get(),
            Name = name,
        };


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public override IEnumerable<string> GetMetadataSources() => Collection.Select(x => x.MetadataSource);

        protected override IVisualizerOptions Clone(string base64)
        {
            return JSON2.FromBase64<RadarVisualizer>(base64);
        }

        public string GetPalette() => Color;

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public string Color
        {
            get => _color;
            set
            {
                SetValue(ref _color, value);
            }
        }

        public int Maximum
        {
            get => _maximum;
            set => SetValue(ref _maximum, value);
        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public override string Id   => "3D44E83D7EB445FB8D70E7A466840889";
        public override string Name => "雷达图";

        public override int MinHeight => 200;
        public override int MinWidth  => 200;

        public override AdjustMode AdjustMode => AdjustMode.Square;
    }
}