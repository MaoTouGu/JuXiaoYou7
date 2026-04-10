using System.Windows.Input;

namespace KinonekoSoftware.UX
{
    public sealed class ForumThread : ContentControl
    {

        public static readonly DependencyProperty PosterNameProperty =
            DependencyProperty.Register(
                                        nameof(PosterName),
                                        typeof(string),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(string)));

        public static readonly DependencyProperty PosterGravatarProperty =
            DependencyProperty.Register(
                                        nameof(PosterGravatar),
                                        typeof(ImageSource),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(ImageBrush)));

        public static readonly DependencyProperty ReplyCommandProperty =
            DependencyProperty.Register(
                                        nameof(ReplyCommand),
                                        typeof(ICommand),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty ReplyCommandTitleProperty =
            DependencyProperty.Register(
                                        nameof(ReplyCommandTitle),
                                        typeof(string),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(string)));


        public static readonly DependencyProperty EditCommandTitleProperty =
            DependencyProperty.Register(
                                        nameof(EditCommandTitle),
                                        typeof(string),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(string)));
        
        public static readonly DependencyProperty RemoveCommandTitleProperty =
            DependencyProperty.Register(
                                        nameof(RemoveCommandTitle),
                                        typeof(string),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(string)));

        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register(
                                        nameof(EditCommand),
                                        typeof(ICommand),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register(
                                        nameof(RemoveCommand),
                                        typeof(ICommand),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty LikeAmountProperty =
            DependencyProperty.Register(
                                        nameof(LikeAmount),
                                        typeof(int),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(int)));

        public static readonly DependencyProperty DislikeAmountProperty =
            DependencyProperty.Register(
                                        nameof(DislikeAmount),
                                        typeof(int),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(int)));


        public static readonly DependencyProperty LikeCommandProperty =
            DependencyProperty.Register(
                                        nameof(LikeCommand),
                                        typeof(ICommand),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty DislikeCommandProperty =
            DependencyProperty.Register(
                                        nameof(DislikeCommand),
                                        typeof(ICommand),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(ICommand)));


        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                                        nameof(CommandParameter),
                                        typeof(object),
                                        typeof(ForumThread),
                                        new PropertyMetadata(default(object)));
        
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public ICommand DislikeCommand
        {
            get => (ICommand)GetValue(DislikeCommandProperty);
            set => SetValue(DislikeCommandProperty, value);
        }
        public ICommand LikeCommand
        {
            get => (ICommand)GetValue(LikeCommandProperty);
            set => SetValue(LikeCommandProperty, value);
        }
        
        public int DislikeAmount
        {
            get => (int)GetValue(DislikeAmountProperty);
            set => SetValue(DislikeAmountProperty, value);
        }
        public int LikeAmount
        {
            get => (int)GetValue(LikeAmountProperty);
            set => SetValue(LikeAmountProperty, value);
        }

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }
        
        public ICommand ReplyCommand
        {
            get => (ICommand)GetValue(ReplyCommandProperty);
            set => SetValue(ReplyCommandProperty, value);
        }
        
        public string RemoveCommandTitle
        {
            get => (string)GetValue(RemoveCommandTitleProperty);
            set => SetValue(RemoveCommandTitleProperty, value);
        }
        
        public string EditCommandTitle
        {
            get => (string)GetValue(EditCommandTitleProperty);
            set => SetValue(EditCommandTitleProperty, value);
        }

        public string ReplyCommandTitle
        {
            get => (string)GetValue(ReplyCommandTitleProperty);
            set => SetValue(ReplyCommandTitleProperty, value);
        }

        public ImageSource PosterGravatar
        {
            get => (ImageSource)GetValue(PosterGravatarProperty);
            set => SetValue(PosterGravatarProperty, value);
        }
        
        public string PosterName
        {
            get => (string)GetValue(PosterNameProperty);
            set => SetValue(PosterNameProperty, value);
        }
    }
}