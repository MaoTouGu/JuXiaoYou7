


namespace KinonekoSoftware.UI.Charts
{
    public class Series : ObservableObject
    {
        private double _value;
        private string _color;

        /// <summary>
        /// 获取或设置 <see cref="Color"/> 属性。
        /// </summary>
        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// 获取或设置 <see cref="Value"/> 属性。
        /// </summary>
        public double Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
    }
}