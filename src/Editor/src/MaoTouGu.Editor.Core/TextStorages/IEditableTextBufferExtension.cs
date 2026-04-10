namespace MaoTouGu.Editor.TextStorages
{
    public interface IEditableTextBufferExtension : IEditableTextBuffer
    {
        /// <summary>
        /// 内容长度。
        /// </summary>
        int Length { get; }

        char this[int index]
        {
            get;
        }
    }
}