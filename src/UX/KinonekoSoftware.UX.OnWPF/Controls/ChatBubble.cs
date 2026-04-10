

using KinonekoSoftware.Foundation;
using MaoTouGu.Foundation;

namespace KinonekoSoftware.UX
{
    public sealed class ChatBubble : ContentControl
    {
        public static readonly DependencyProperty ActorNameProperty =
            DependencyProperty.Register(
                                        nameof(ActorName),
                                        typeof(string),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(string)));


        public static readonly DependencyProperty ActorTitleProperty =
            DependencyProperty.Register(
                                        nameof(ActorTitle),
                                        typeof(string),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(string)));


        public static readonly DependencyProperty IsSelfProperty =
            DependencyProperty.Register(
                                        nameof(IsSelf),
                                        typeof(bool),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(Boxing.False));


        public static readonly DependencyProperty ActorGravatarProperty =
            DependencyProperty.Register(
                                        nameof(ActorGravatar),
                                        typeof(Brush),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(Brush)));



        public static readonly DependencyProperty ActorTitleBrushProperty =
            DependencyProperty.Register(
                                        nameof(ActorTitleBrush),
                                        typeof(Brush),
                                        typeof(ChatBubble),
                                        new PropertyMetadata(default(Brush)));

        public Brush ActorTitleBrush
        {
            get => (Brush)GetValue(ActorTitleBrushProperty);
            set => SetValue(ActorTitleBrushProperty, value);
        }
        public bool IsSelf
        {
            get => (bool)GetValue(IsSelfProperty);
            set => SetValue(IsSelfProperty, Boxing.Box(value));
        }

        public string ActorTitle
        {
            get => (string)GetValue(ActorTitleProperty);
            set => SetValue(ActorTitleProperty, value);
        }

        public Brush ActorGravatar
        {
            get => (Brush)GetValue(ActorGravatarProperty);
            set => SetValue(ActorGravatarProperty, value);
        }

        public string ActorName
        {
            get => (string)GetValue(ActorNameProperty);
            set => SetValue(ActorNameProperty, value);
        }
    }
}