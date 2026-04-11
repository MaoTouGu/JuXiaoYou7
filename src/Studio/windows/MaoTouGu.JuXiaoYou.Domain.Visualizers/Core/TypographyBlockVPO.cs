// ----------------------------------------------------------
//            文件：TypographyBlockVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 20:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public abstract class TypographyBlockVPO : ObservableObjectEX<JuXiaoYouPage>
    {
        private IVisualizerOptions _options;

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


        public IVisualizerOptions Options
        {
            get => _options;
            set => SetValue(ref _options, value);
        }
    }
}