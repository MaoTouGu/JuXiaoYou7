namespace MaoTouGu.Shells.Services
{
    public interface IGuideElementRecipient
    {
        void Accept(FrameworkElement element);

        void Clear();
    }
}