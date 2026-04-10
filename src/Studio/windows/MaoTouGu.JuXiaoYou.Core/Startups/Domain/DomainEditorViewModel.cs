// ----------------------------------------------------------
//            文件：DomainEditorViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月30日 00:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Startups
{
    public class DomainEditorViewModel : ObjectRoot<bool>
    {
        private readonly Domain _domain;
        private readonly int    _hashCode;

        public DomainEditorViewModel(Domain domain)
        {
            _domain   = domain;
            _hashCode = domain.GetHashCode();

            Select = new SelectDomainImageCommand(this);
        }

        protected override bool OnFinish(bool edit) => _hashCode != _domain.GetHashCode();

        public Domain Domain => _domain;

        public int Y
        {
            get => _domain.Y;
            set
            {
                _domain.Y = value;
                RaiseUpdated();
            }
        }

        public int X
        {
            get => _domain.X;
            set
            {
                _domain.X = value;
                RaiseUpdated();
            }
        }

        public int ImageHeight
        {
            get => _domain.ImageHeight;
            set
            {
                _domain.ImageHeight = value;
                RaiseUpdated();
            }
        }
        
        public int ImageWidth
        {
            get => _domain.ImageWidth;
            set
            {
                _domain.ImageWidth = value;
                RaiseUpdated();
            }
        }
        
        public int Height
        {
            get => _domain.Height;
            set
            {
                _domain.Height = value;
                RaiseUpdated();
            }
        }
        
        public int Width
        {
            get => _domain.Width;
            set
            {
                _domain.Width = value;
                RaiseUpdated();
            }
        }
        
        public string Image
        {
            get => _domain.Image;
            set
            {
                _domain.Image = value;
                RaiseUpdated();
            }
        }

        public string Name
        {
            get => _domain.Name;
            set
            {
                _domain.Name = value;
                RaiseUpdated();
            }
        }
        
        public ICommandEX Select { get; }
    }
}