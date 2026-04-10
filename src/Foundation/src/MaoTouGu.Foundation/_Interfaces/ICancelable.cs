namespace MaoTouGu.Foundation
{
    /// <summary>
    /// <see cref="ICancelable"/> 接口继承了<see cref="IDisposable"/>接口，并在此之上提供了<see cref="IsDisposed"/>属性，用于判断该接口是否已经释放。
    /// </summary>
    public interface ICancelable : IDisposable
    {
        /// <summary>
        /// 判断是否已经释放。
        /// </summary>
        bool IsDisposed { get; }
    }
}