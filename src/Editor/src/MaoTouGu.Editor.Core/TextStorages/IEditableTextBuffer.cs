namespace MaoTouGu.Editor.TextStorages
{
    public interface IEditableTextBuffer : ITextBuffer
    {
        /// <summary>
        /// 添加字符串。
        /// </summary>
        /// <param name="text">要添加的文本。</param>
        void Add(string text);

        /// <summary>
        /// 在指定位置插入字符串。
        /// </summary>
        /// <param name="index">指定要插入文本的位置。</param>
        /// <param name="text">要插入的文本。</param>
        void Insert(int index, string text);

        /// <summary>
        /// 替换字符串。
        /// </summary>
        /// <param name="text">要替换的文本。</param>
        /// <param name="offset">被替换的文本内容起始位置。</param>
        /// <param name="length">被替换的文本内容总长度。</param>
        string Replace(string text, int offset, int length);

        /// <summary>
        /// 从指定位置开始删除内容。
        /// </summary>
        /// <param name="offset">指定开始删除的位置。</param>
        string Remove(int offset);

        /// <summary>
        /// 删除指定范围的文本字符串。
        /// </summary>
        /// <param name="offset">文本的起始点。</param>
        /// <param name="length">要删除的内容长度。</param>
        string Remove(int offset, int length);
        
        /// <summary>
        /// 清空所有内容。
        /// </summary>
        void Clear();
        
        /// <summary>
        /// 判断是否所有文本内容都是相同字符。
        /// </summary>
        /// <param name="symbol">输出这个相同的字符。</param>
        /// <returns>返回是与否。</returns>
        bool All(out char symbol);
        
        /// <summary>
        /// IndexOfAny方法的实现。
        /// </summary>
        /// <param name="anyOf">Characters to search for</param>
        /// <param name="startIndex">Start index of the area to search.</param>
        /// <param name="count">Length of the area to search.</param>
        /// <returns>The first index where any character was found; or -1 if no occurrence was found.</returns>
        int IndexOfAny(char[] anyOf, int startIndex, int count);
        
        /// <summary>
		/// Gets the index of the first occurrence of the specified search text in this text source.
		/// </summary>
		/// <param name="searchText">The search text</param>
		/// <param name="startIndex">Start index of the area to search.</param>
		/// <param name="count">Length of the area to search.</param>
		/// <param name="comparisonType">String comparison to use.</param>
		/// <returns>The first index where the search term was found; or -1 if no occurrence was found.</returns>
		int IndexOf(string searchText, int startIndex, int count, StringComparison comparisonType);

		/// <summary>
		/// Gets the index of the last occurrence of the specified character in this text source.
		/// </summary>
		/// <param name="c">The search character</param>
		/// <param name="startIndex">Start index of the area to search.</param>
		/// <param name="count">Length of the area to search.</param>
		/// <returns>The last index where the search term was found; or -1 if no occurrence was found.</returns>
		/// <remarks>The search proceeds backwards from (startIndex+count) to startIndex.
		/// This is different than the meaning of the parameters on string.LastIndexOf!</remarks>
		int LastIndexOf(char c, int startIndex, int count);

		/// <summary>
		/// Gets the index of the last occurrence of the specified search text in this text source.
		/// </summary>
		/// <param name="searchText">The search text</param>
		/// <param name="startIndex">Start index of the area to search.</param>
		/// <param name="count">Length of the area to search.</param>
		/// <param name="comparisonType">String comparison to use.</param>
		/// <returns>The last index where the search term was found; or -1 if no occurrence was found.</returns>
		/// <remarks>The search proceeds backwards from (startIndex+count) to startIndex.
		/// This is different than the meaning of the parameters on string.LastIndexOf!</remarks>
		int LastIndexOf(string searchText, int startIndex, int count, StringComparison comparisonType);
    }
}