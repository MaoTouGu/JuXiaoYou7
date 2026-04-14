// ----------------------------------------------------------
//            文件：TypographyBlockVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 20:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public abstract class TypographyBlockVPO : ObservableObjectEX<JuXiaoYouPage>, INotifyPropertyChangedEX
    {
        private IVisualizerOptions _options;

        private static readonly List<TypographyBlockVPO> _dummies = new List<TypographyBlockVPO>
        {
            new TypographyVisualizerVPO { Moniker = null },
            new TypographyImageVPO { Moniker      = null },
            new TypographyTextVPO { Moniker       = null },
        };


        public static TypographyBlockVPO GetInstance(TypographyBlock block, Moniker moniker)
        {
            return _dummies.FirstOrDefault(x => x.CanAccept(block))
                          ?.OnCreate(block, moniker);
        }

        protected abstract bool CanAccept(TypographyBlock block);

        protected abstract TypographyBlockVPO OnCreate(TypographyBlock block, Moniker moniker);


        public void RaisePropertyChanged(string name)
        {
            RaiseUpdated(name);
        }

        public string Id => Base.Id;

        public TypographyBlock Base { get; protected init; }

        public required Moniker Moniker { get; init; }

        public double Height
        {
            get => Base.Height;
            set
            {
                Base.Height = value;
                RaiseUpdated();
            }
        }

        public double Width
        {
            get => Base.Width;
            set
            {
                Base.Width = value;
                RaiseUpdated();
            }
        }

        public double Y
        {
            get => Base.Y;
            set
            {
                Base.Y = value;
                RaiseUpdated();
            }
        }

        public double X
        {
            get => Base.X;
            set
            {
                Base.X = value;
                RaiseUpdated();
            }
        }

        public bool IsLock
        {
            get => Base.IsLock;
            set
            {
                Base.IsLock = value;
                RaiseUpdated();
            }
        }

        public double Opacity
        {
            get => Base.Opacity;
            set
            {
                Base.Opacity = value;
                RaiseUpdated();
            }
        }

        public IVisualizerOptions Options
        {
            get => _options;
            set
            {
                SetValue(ref _options, value);

                if (_options is VisualizerOptions vo)
                {
                    vo.FactoryInternal = () => ViewModel;
                }
            }
        }
    }
}