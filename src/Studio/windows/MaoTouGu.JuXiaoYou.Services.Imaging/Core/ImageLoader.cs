using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Xaml.Behaviors;
using Image = System.Windows.Controls.Image;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public partial class ImageLoader : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 为支持的控件类型设置图片。
        /// </summary>
        /// <param name="fe">未强转类型之前的控件类型，可能是支持的控件类型。</param>
        /// <param name="image">图片源</param>
        static void SetImage(FrameworkElement fe, BitmapImage image)
        {
            if (fe is IImageWorker worker)
            {
                worker.SetImage(image);
            }
            if (fe is Panel)
            {
                SetPanelWorker(fe, image);
            }
            else if (fe is Control)
            {
                SetControlWorker(fe, image);
            }
            else if (fe is Border)
            {
                SetBorderWorker(fe, image);
            }
            else if (fe is Image)
            {
                SetImageWorker(fe, image);
            }
        }
        
        /// <summary>
        /// 为<see cref="Border"/>控件设置图片。
        /// </summary>
        /// <param name="fe">未强转类型之前的控件类型，必须是<see cref="Border"/>类型。</param>
        /// <param name="image">图片源</param>
        static void SetBorderWorker(FrameworkElement fe, BitmapImage image)
        {
            var border = (Border)fe;

            border.Background = new ImageBrush
            {
                ImageSource = image,
            };
        }
        
        /// <summary>
        /// 为<see cref="Control"/>控件设置图片。
        /// </summary>
        /// <param name="fe">未强转类型之前的控件类型，必须是<see cref="Control"/>类型。</param>
        /// <param name="image">图片源</param>
        static void SetControlWorker(FrameworkElement fe, BitmapImage image)
        {
            var border = (Control)fe;

            border.Background = new ImageBrush
            {
                ImageSource = image,
            };
        }    

        
        /// <summary>
        /// 为<see cref="Image"/>控件设置图片。
        /// </summary>
        /// <param name="fe">未强转类型之前的控件类型，必须是<see cref="Image"/>类型。</param>
        /// <param name="image">图片源</param>
        static void SetImageWorker(FrameworkElement fe, BitmapImage image)
        {
            var border = (Image)fe;

            border.Source  = image;
        }    
        
        /// <summary>
        /// 为<see cref="Panel"/>控件设置图片。
        /// </summary>
        /// <param name="fe">未强转类型之前的控件类型，必须是<see cref="Panel"/>类型。</param>
        /// <param name="image">图片源</param>
        static void SetPanelWorker(FrameworkElement fe, BitmapImage image)
        {
            var border = (Panel)fe;

            border.Background = new ImageBrush
            {
                ImageSource = image,
            };
        }
        
        public async Task Update()
        {
            try
            {
                if (string.IsNullOrEmpty(Source))
                {
                    SetImage(AssociatedObject, ImageSystem.GetFallbackImage(Fallback));
                    return;
                }

                var r = ImageSystem.LRU(Source, Thumbnail);

                //
                // 判断缓存中是否存在指定的图片。
                if (r is not null)
                {
                    SetImage(AssociatedObject, r);
                    return;
                }

                //
                // 直接创建Bitmap
                var image = await ImageSystem.Load(Source, Thumbnail, Fallback, Dir);

                if (image is not null)
                {
                    SetImage(AssociatedObject, image);
                    return;
                }

                SetImage(AssociatedObject, ImageSystem.GetFallbackImage(Fallback));
            }
            catch
            {
                SetImage(AssociatedObject, ImageSystem.GetFallbackImage(Fallback));
            }
        }

        public string Dir => ImageSystem.GetDir(Type);

        private ImageType           _type;
        private ThumbnailLevel      _thumbnail;
        private ImageFallbackOption _fallback;
        private string              _source;

        public string Source
        {
            get => _source;
            set
            {
                _source = value;
            }
        }

        public ThumbnailLevel Thumbnail
        {
            get => _thumbnail;
            set
            {
                _thumbnail = value;
            }
        }

        public ImageType Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        public ImageFallbackOption Fallback
        {
            get => _fallback;
            set
            {
                _fallback = value;
            }
        }
    }
}