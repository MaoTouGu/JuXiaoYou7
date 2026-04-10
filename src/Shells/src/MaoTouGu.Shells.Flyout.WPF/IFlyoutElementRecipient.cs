namespace MaoTouGu.Shells.Services
{
    /// <summary>
    /// <see cref="IFlyoutElementRecipient"/> 接口用于表示Flyout元素的接收者，通常而言由<see cref="MTGWindow"/>继承并实现。
    /// </summary>
    public interface IFlyoutElementRecipient
    {
        void Accept(FrameworkElement element);

        void Clear();
    }
}