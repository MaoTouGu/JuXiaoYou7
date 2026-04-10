namespace MaoTouGu.Foundation
{
    /// <summary>
    /// <see cref="ICloneable{T}"/> 类型接口在<see cref="ICloneable"/>接口的基础上，提供了泛型返回值方法。
    /// </summary>
    /// <typeparam name="T">需要克隆的数据类型。</typeparam>
    public interface ICloneable<T>
    {
        T Clone();
    }
}