namespace KinonekoSoftware.UX
{
    public class EmptyTextView : Control
    {
        static EmptyTextView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EmptyTextView), new FrameworkPropertyMetadata(typeof(EmptyTextView)));
        }


        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(
                                        nameof(Image),
                                        typeof(ImageSource),
                                        typeof(EmptyTextView),
                                        new PropertyMetadata(default(ImageSource)));


        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                                        nameof(Text),
                                        typeof(string),
                                        typeof(EmptyTextView),
                                        new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
    }
}