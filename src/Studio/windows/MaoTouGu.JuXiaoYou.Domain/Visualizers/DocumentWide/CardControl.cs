// ----------------------------------------------------------
//            文件：CardControl.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月08日 14:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KinonekoSoftware.UI;
using MaoTouGu.JuXiaoYou.Controls;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.Gaming.Arcadia.Controls
{
    public abstract class CardControl : UserControl
    {

        public static readonly DependencyProperty ThumbnailProperty =
            DependencyProperty.Register(
                                        nameof(Thumbnail),
                                        typeof(ThumbnailLevel?),
                                        typeof(CardControl),
                                        new PropertyMetadata(null));
        protected CardControl()
        {
            Loaded += OnLoaded;
        }
        
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var image = Xaml.FindVisualChild<ImageEX>(this, x => x.Name == nameof(Image));

            if(image?.DataContext is not Moniker dc)
            {
                return;
            }
            
            ImageSystem.SetFallback(image, ImageFallbackOption.Image);
            ImageSystem.SetType(image, ImageType.Image);
            ImageSystem.SetThumbnail(image, Thumbnail);

            image.SetBinding(ImageSystem.SourceProperty, new Binding
            {
                Source = dc,
                Path   = new PropertyPath(nameof(Image)),
                Mode   = BindingMode.OneWay,
            });
        }
        

        public ThumbnailLevel Thumbnail
        {
            get => (ThumbnailLevel)GetValue(ThumbnailProperty);
            set => SetValue(ThumbnailProperty, value);
        }
    }
}