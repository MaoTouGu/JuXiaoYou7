// ----------------------------------------------------------
//            文件：Source.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    partial class ImageSystem
    {
#if NEXT_VERSION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static object OnSourceChanged(DependencyObject d, object baseValue) => baseValue;
#endif
        
        /// <summary>
        /// 附加属性的定义
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached(
                                                "Source",
                                                typeof(string),
                                                typeof(ImageSystem),
                                                new PropertyMetadata(DefaultImageUri, OnImageSourceChanged));
        
        /// <summary>
        /// 附加属性的Setter方法。
        /// </summary>
        /// <param name="element">控件</param>
        /// <param name="value">值</param>
        public static void SetSource(DependencyObject element, string value)
        {
            element.SetValue(SourceProperty, value);
        }

        /// <summary>
        /// 附加属性的Getter方法。
        /// </summary>
        /// <param name="element">控件。</param>
        /// <returns>值</returns>
        public static string GetSource(DependencyObject element)
        {
            return (string)element.GetValue(SourceProperty);
        }



        /// <summary>
        /// 附加属性的定义
        /// </summary>
        private static async void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var collection = Interaction.GetBehaviors(d);
            var loader     = collection.OfType<ImageLoader>().FirstOrDefault();

            if (loader is null)
            {
                loader = new ImageLoader
                {
                    Type      = GetType(d),
                    Thumbnail = GetThumbnail(d),
                    Fallback  = GetFallback(d),
                };

                collection.Add(loader);
            }

            //
            // 设置Source
            loader.Source = e.NewValue
                            ?.ToString();

            //
            // 更新
            await loader.Update();
        }
    }
}