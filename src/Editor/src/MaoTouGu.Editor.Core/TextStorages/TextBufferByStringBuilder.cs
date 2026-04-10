namespace MaoTouGu.Editor.TextStorages
{
    /// <summary>
    /// <see cref="TextBufferByStringBuilder"/> 类型用于高效维护文本的添加、删除、替换、合并操作。
    /// </summary>
    public class TextBufferByStringBuilder : IEditableTextBufferExtension
    {
        private StringBuilder _sb = new StringBuilder();
        

        /// <summary>
        /// 添加字符串。
        /// </summary>
        /// <param name="text">要添加的文本。</param>
        public void Add(string text)
        {
            //
            //
            _sb = _sb.Append(text);
        }
        
        /// <summary>
        /// 在指定位置插入字符串。
        /// </summary>
        /// <param name="index">指定要插入文本的位置。</param>
        /// <param name="text">要插入的文本。</param>
        public void Insert(int index, string text)
        {
            //
            //
            _sb = _sb.Insert(index, text);
        }

        /// <summary>
        /// 替换字符串。
        /// </summary>
        /// <param name="text">要替换的文本。</param>
        /// <param name="offset">被替换的文本内容起始位置。</param>
        /// <param name="length">被替换的文本内容总长度。</param>
        public string Replace(string text, int offset, int length)
        {
            var oldString = GetText(offset, length);
            _sb = _sb.Replace(oldString, text, offset, length);

            return oldString;
        }

        /// <summary>
        /// 从指定位置开始删除内容。
        /// </summary>
        /// <param name="offset">指定开始删除的位置。</param>
        public string Remove(int offset)
        {
            var removal = _sb.ToString(offset, Length - offset);
            _sb = _sb.Remove(offset, Length - offset);
            return removal;
        }

        /// <summary>
        /// 删除指定范围的文本字符串。
        /// </summary>
        /// <param name="offset">文本的起始点。</param>
        /// <param name="length">要删除的内容长度。</param>
        public string Remove(int offset, int length)
        {
            var removal = _sb.ToString(offset, length);
            _sb = _sb.Remove(offset, length);
            return removal;
        }

        /// <summary>
        /// 清空所有内容。
        /// </summary>
        public void Clear()
        {
            _sb = _sb.Clear();
        }
        
        /// <summary>
        /// 判断是否所有文本内容都是相同字符。
        /// </summary>
        /// <param name="symbol">输出这个相同的字符。</param>
        /// <returns>返回是与否。</returns>
        public bool All(out char symbol)
        {
            if (_sb.Length == 0)
            {
                symbol = '\x20';
                return false;
            }
            
            for (var i = 1; i < _sb.Length; i++)
            {
                if (_sb[i] != _sb[i - 1])
                {
                    symbol = '\x20';
                    return false;
                }
            }

            symbol = _sb[0];
            return true;
        }

        /// <summary>
        /// 获得所有文本内容。
        /// </summary>
        /// <returns>返回一个新的字符串对象实例。</returns>
        public string GetText() => _sb.ToString();

        /// <summary>
        /// 获得从指定位置开始到文本结尾的内容。
        /// </summary>
        /// <param name="offset">指定开始获取的位置。</param>
        /// <returns>返回一个新的字符串对象实例。</returns>
        public string GetText(int offset)
        {
            if (Length - offset < 0)
            {
                return "\uf8fe";
            }
            
            return _sb.ToString(offset, Length - offset);
        }

        /// <summary>
        /// 获得指定范围的文本内容。
        /// </summary>
        /// <param name="offset">文本的起始点。</param>
        /// <param name="length">要获取的内容长度。</param>
        /// <returns>返回一个新的字符串对象实例。</returns>
        public string GetText(int offset, int length)
        {
            return _sb.ToString(offset, length);
        }

        /// <summary>
        /// 获得指定索引位置的单个字符。
        /// </summary>
        /// <param name="offset">指定的索引位置。</param>
        /// <returns>返回一个字符。</returns>
        public char GetCharAt(int offset) => _sb[offset];

        /// <summary>
        /// IndexOfAny方法的实现。
        /// </summary>
        /// <param name="anyOf">Characters to search for</param>
        /// <param name="startIndex">Start index of the area to search.</param>
        /// <param name="count">Length of the area to search.</param>
        /// <returns>The first index where any character was found; or -1 if no occurrence was found.</returns>
        public int IndexOfAny(char[] anyOf, int startIndex, int count) 
        {
            return _sb.ToString()
                      .IndexOfAny(anyOf, startIndex, count);
        }

        /// <summary>
        /// Gets the index of the first occurrence of the specified search text in this text source.
        /// </summary>
        /// <param name="searchText">The search text</param>
        /// <param name="startIndex">Start index of the area to search.</param>
        /// <param name="count">Length of the area to search.</param>
        /// <param name="comparisonType">String comparison to use.</param>
        /// <returns>The first index where the search term was found; or -1 if no occurrence was found.</returns>
        public int IndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
        {
            return _sb.ToString()
                      .IndexOf(searchText, startIndex, count, comparisonType);
        }

        /// <summary>
        /// Gets the index of the last occurrence of the specified character in this text source.
        /// </summary>
        /// <param name="c">The search character</param>
        /// <param name="startIndex">Start index of the area to search.</param>
        /// <param name="count">Length of the area to search.</param>
        /// <returns>The last index where the search term was found; or -1 if no occurrence was found.</returns>
        /// <remarks>The search proceeds backwards from (startIndex+count) to startIndex.
        /// This is different than the meaning of the parameters on string.LastIndexOf!</remarks>
        public int LastIndexOf(char c, int startIndex, int count)
        {
            return _sb.ToString()
                      .LastIndexOf(c, startIndex, count);
        }

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
        public int LastIndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
        {
            return _sb.ToString()
                      .LastIndexOf(searchText, startIndex, count, comparisonType);
        }

        public char this[int index]
        {
            get => _sb[index];
        }
        /// <summary>
        /// 内容长度。
        /// </summary>
        public int Length => _sb.Length;
    }
}