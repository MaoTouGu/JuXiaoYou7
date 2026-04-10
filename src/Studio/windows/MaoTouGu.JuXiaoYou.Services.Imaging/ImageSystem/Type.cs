// ----------------------------------------------------------
//            文件：Type.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:20
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
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.RegisterAttached(
                                                "Type",
                                                typeof(ImageType),
                                                typeof(ImageSystem),
                                                new PropertyMetadata(default(ImageType), OnTypeChanged));
        /// <summary>
        /// 附加属性的Setter方法。
        /// </summary>
        /// <param name="element">控件</param>
        /// <param name="value">值</param>

        public static void SetType(DependencyObject element, ImageType value)
        {
            element.SetValue(TypeProperty, value);
        }

        /// <summary>
        /// 附加属性的Getter方法。
        /// </summary>
        /// <param name="element">控件。</param>
        /// <returns>值</returns>
        public static ImageType GetType(DependencyObject element)
        {
            return (ImageType)element.GetValue(TypeProperty);
        }

        
        /// <summary>
        /// 附加属性的定义
        /// </summary>
        private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Interaction.GetBehaviors(d)
                       .OfType<ImageLoader>()
                       .ForEach(async x =>
                                {
                                    x.Type = (ImageType)e.NewValue;
                                    await x.Update();
                                });
        } 
    }
}