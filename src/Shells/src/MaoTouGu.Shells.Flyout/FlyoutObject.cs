using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.Core
{
    public sealed class FlyoutObject : ObservableObject, IComparable<FlyoutObject>
    {
        private bool      _showNextStep;
        private Placement _placement;
        private string    _color;
        private string    _content;
        private string    _title;
        private string    _buttonText;

        public int CompareTo(FlyoutObject other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (other is null)
            {
                return 1;
            }

            return Index.CompareTo(other.Index);
        }

        internal object Window { get; set; }
        internal object View   { get; set; }
        internal int    Index  { get; set; }


         
        public bool ShowNextStep
        {
            get => _showNextStep;
            set => SetValue(ref _showNextStep, value);
        }
        public Placement ComputedPlacement
        {
            get
            {
                return Placement switch
                {
                    Placement.Left  => Placement.Right,
                    Placement.Right => Placement.Left,
                    Placement.Top   => Placement.Bottom,
                    _               => Placement.Top,
                };
            }
        }


        /// <summary>
        /// 停靠。
        /// </summary>
        public Placement Placement
        {
            get => _placement;
            set
            {
                SetValue(ref _placement, value);
                RaiseUpdated(nameof(ComputedPlacement));
            }
        }

        /// <summary>
        /// 颜色。
        /// </summary>
        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content
        {
            get => _content;
            set => SetValue(ref _content, value);
        }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }

        /// <summary>
        /// 按钮标题。
        /// </summary>
        public string ButtonText
        {
            get => _buttonText;
            set => SetValue(ref _buttonText, value);
        }
    }
}