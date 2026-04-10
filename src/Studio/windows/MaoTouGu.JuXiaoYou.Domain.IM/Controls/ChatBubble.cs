// ----------------------------------------------------------
//            文件：ChatBubble.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月11日 02:13
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Imaging;

namespace MaoTouGu.JuXiaoYou.Domain.IM.Controls
{
    public class ChatBubble : ContentControl, IImageWorker
    {
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register(
                                        nameof(DisplayName),
                                        typeof(string),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(string)));


        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                                        nameof(Title),
                                        typeof(string),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(string)));


        public static readonly DependencyProperty IsSelfProperty =
            DependencyProperty.Register(
                                        nameof(IsSelf),
                                        typeof(bool),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(Boxing.False));


        public static readonly DependencyProperty GravatarProperty =
            DependencyProperty.Register(
                                        nameof(Gravatar),
                                        typeof(Brush),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(Brush)));



        public static readonly DependencyProperty TitleBrushProperty =
            DependencyProperty.Register(
                                        nameof(TitleBrush),
                                        typeof(Brush),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(Brush)));

        public void SetImage(BitmapImage bi)
        {
            Gravatar = new ImageBrush
            {
                ImageSource = bi,
            };
        }

        public Brush TitleBrush
        {
            get => (Brush)GetValue(TitleBrushProperty);
            set => SetValue(TitleBrushProperty, value);
        }
        public bool IsSelf
        {
            get => (bool)GetValue(IsSelfProperty);
            set => SetValue(IsSelfProperty, Boxing.Box(value));
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Brush Gravatar
        {
            get => (Brush)GetValue(GravatarProperty);
            set => SetValue(GravatarProperty, value);
        }

        public string DisplayName
        {
            get => (string)GetValue(DisplayNameProperty);
            set => SetValue(DisplayNameProperty, value);
        }

    }
}