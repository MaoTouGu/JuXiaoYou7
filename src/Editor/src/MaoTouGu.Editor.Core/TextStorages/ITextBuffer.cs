namespace MaoTouGu.Editor.TextStorages
{
    public interface ITextBuffer
    {
        /// <summary>
        /// 获得所有文本内容。
        /// </summary>
        /// <returns>返回一个新的字符串对象实例。</returns>
        string GetText();

        /// <summary>
        /// 获得从指定位置开始到文本结尾的内容。
        /// </summary>
        /// <param name="offset">指定开始获取的位置。</param>
        /// <returns>返回一个新的字符串对象实例。</returns>
        string GetText(int offset);

        /// <summary>
        /// 获得指定范围的文本内容。
        /// </summary>
        /// <param name="offset">文本的起始点。</param>
        /// <param name="length">要获取的内容长度。</param>
        /// <returns>返回一个新的字符串对象实例。</returns>
        string GetText(int offset, int length);

        /// <summary>
        /// 获得指定索引位置的单个字符。
        /// </summary>
        /// <param name="offset">指定的索引位置。</param>
        /// <returns>返回一个字符。</returns>
        char GetCharAt(int offset);
    }
}