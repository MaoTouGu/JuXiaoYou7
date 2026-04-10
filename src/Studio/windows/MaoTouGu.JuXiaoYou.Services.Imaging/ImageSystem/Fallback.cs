// ----------------------------------------------------------
//            文件：Fallback.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    partial class ImageSystem
    {
        /// <summary>
        /// 附加属性的定义
        /// </summary>
        public static readonly DependencyProperty FallbackProperty =
            DependencyProperty.RegisterAttached(
                                                "Fallback",
                                                typeof(ImageFallbackOption),
                                                typeof(ImageSystem),
                                                new PropertyMetadata(default(ImageFallbackOption), OnFallbackChanged));
        

        /// <summary>
        /// 附加属性的Setter方法。
        /// </summary>
        /// <param name="element">控件</param>
        /// <param name="value">值</param>
        public static void SetFallback(DependencyObject element, ImageFallbackOption value)
        {
            element.SetValue(FallbackProperty, value);
        }

        /// <summary>
        /// 附加属性的Getter方法。
        /// </summary>
        /// <param name="element">控件。</param>
        /// <returns>值</returns>
        public static ImageFallbackOption GetFallback(DependencyObject element)
        {
            return (ImageFallbackOption)element.GetValue(FallbackProperty);
        }

        /// <summary>
        /// 附加属性的定义
        /// </summary>
        private static void OnFallbackChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Interaction.GetBehaviors(d)
                       .OfType<ImageLoader>()
                       .ForEach(async x =>
                                {
                                    x.Fallback = (ImageFallbackOption)e.NewValue;
                                    await x.Update();
                                });
        }      

    }
}