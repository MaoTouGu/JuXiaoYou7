// ----------------------------------------------------------
//            文件：AddImageBlockCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 17:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;

namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddImageBlockCommand(DesignViewModel target) : VisualizerCommand(target)
    {
        public override async void Execute(object parameter)
        {
            if (!Verify())
            {
                return;
            }

            var r = Interop.OpenFileAsync(SR.Image_Png);

            if (!r.IsFinished)
            {
                return;
            }

            //
            //
            var visualizer = new TypographyImage
            {
                Id     = ID.Get(),
                Name   = "文本",
                Width  = 100,
                Height = 100,
                Source = ID.Get(),
            };


            //
            //
            try
            {
                var buffer = await File.ReadAllBytesAsync(r.Value);
                var base64 = Convert.ToBase64String(buffer);
                var bi     = Xaml.ToBitmap(buffer);

                Context.Bitmaps.Add(new NamedBitmap
                {
                    Image = bi,
                    Name  = visualizer.Source,
                });

                if (Context.Template
                           .Base64Table
                           .TryAdd(visualizer.Source, base64))
                {
                    //
                    //
                    AppendVisualizer(visualizer);
                }


            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}