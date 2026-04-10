// ----------------------------------------------------------
//            文件：ShareProjectViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 02:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Imaging;
using SixLabors.ImageSharp.Processing;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public sealed class ShareProjectViewModel : FlyoutRoot
    {
        private readonly Project _project;
        
        private BitmapImage _qrCode;
        
        public ShareProjectViewModel(Project project)
        {
            _project = project;
            Save     = new SaveProjectCommand(this);
        }


        protected override async void OnStart()
        {
            var json = JSON.Serialize(_project);
            
            //
            //
            QrCode = await QR.GenerateAsync(json);
            
            
            //
            //
            base.OnStart();
        }

        public Project Project => _project;
        
        public ICommandEX Save { get; }

        public BitmapImage QrCode
        {
            get => _qrCode;
            set => SetValue(ref _qrCode, value);
        }
    }
}