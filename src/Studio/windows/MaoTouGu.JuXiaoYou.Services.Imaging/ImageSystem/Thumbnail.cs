// ----------------------------------------------------------
//            文件：Thumbnail.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 16:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------



namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static partial class ImageSystem
    {
        /// <summary>
        /// 附加属性的定义
        /// </summary>
        public static readonly DependencyProperty ThumbnailProperty =
            DependencyProperty.RegisterAttached(
                                                "Thumbnail",
                                                typeof(ThumbnailLevel),
                                                typeof(ImageSystem),
                                                new PropertyMetadata(default(ThumbnailLevel), OnThumbnailChanged));
        /// <summary>
        /// 附加属性的Setter方法。
        /// </summary>
        /// <param name="element">控件</param>
        /// <param name="value">值</param>
        public static void SetThumbnail(DependencyObject element, ThumbnailLevel value)
        {
            element.SetValue(ThumbnailProperty, value);
        }

        /// <summary>
        /// 附加属性的Getter方法。
        /// </summary>
        /// <param name="element">控件。</param>
        /// <returns>值</returns>
        public static ThumbnailLevel GetThumbnail(DependencyObject element)
        {
            return (ThumbnailLevel)element.GetValue(ThumbnailProperty);
        }    

        /// <summary>
        /// 附加属性变更时的回调方法。
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnThumbnailChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Interaction.GetBehaviors(d)
                       .OfType<ImageLoader>()
                       .ForEach(async x =>
                                {
                                    x.Thumbnail = (ThumbnailLevel)e.NewValue;
                                    await x.Update();
                                });
        }       
    }
}