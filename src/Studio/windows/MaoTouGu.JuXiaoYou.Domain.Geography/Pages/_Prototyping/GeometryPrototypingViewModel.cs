// ----------------------------------------------------------
//            文件：GeometryPrototypingViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月16日 13:13
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using MaoTouGu.JuXiaoYou.Domain.Geography.Core;
using MaoTouGu.JuXiaoYou.Domain.Geography.Models;
using MaoTouGu.Shells.Inputs;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Domain.Geography.Pages
{
    public sealed class GeometryPrototypingViewModel : JuXiaoYouPage
    {
        private Geometry           _geometry;
        private BitmapImage        _raw;
        private GeographyPrototype _prototype;
        
        public GeometryPrototypingViewModel()
        {
            PickImage = new DelegateCommand(DoPickImage);
        }


        private  void DoPickImage()
        {
            var r      = Interop.OpenFileAsync(Shells.SR.Image_Png);
            var busySM = this.AcquireBusyState();
            
            if (!r.IsFinished)
            {
                return;
            }
            
            

            busySM.Execute(new GeometryRecognitionOperation(r.Value, this, OnRecognitionCompleted))
                  .Execute();
        }
        
        private async void OnRecognitionCompleted(string fileName, string literalString, int w, int h, Geometry geometry)
        {
            var ms = new MemoryStream();
            var fs = File.Open(fileName, FileMode.Open);

            await fs.CopyToAsync(ms);
            
            GUI.RunOnUIThread(() =>
            {
                var bi = new BitmapImage();

                //
                //
                ms.Seek(0, SeekOrigin.Begin);

                //
                //
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                Raw = bi;
                Geometry = geometry;
                Prototype = new GeographyPrototype
                {
                    Id = ID.Get(),
                    Width = w,
                    Height = h,
                    Color = "#007ACC",
                    Points = literalString,
                };
            });
        }

        public GeographyPrototype Prototype
        {
            get => _prototype;
            set => SetValue(ref _prototype, value);
        }

        public BitmapImage Raw
        {
            get => _raw;
            set => SetValue(ref _raw, value);
        }

        public Geometry Geometry
        {
            get => _geometry;
            set => SetValue(ref _geometry, value);
        }
        
        public ICommandEX PickImage { get; }
    }
}