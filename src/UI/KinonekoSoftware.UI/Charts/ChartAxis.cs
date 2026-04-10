using System.Drawing;
using MaoTouGu.Foundation;

namespace KinonekoSoftware.UI.Charts
{
    public class ChartAxis : ObservableObject
    {
        private string _name;
        private string _color;

        public double X  { get; set; }
        public double Y  { get; set; }
        
        public string Id { get; init; }

        /// <summary>
        /// 获取或设置 <see cref="Color"/> 属性。
        /// </summary>
        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="Name"/> 属性。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}