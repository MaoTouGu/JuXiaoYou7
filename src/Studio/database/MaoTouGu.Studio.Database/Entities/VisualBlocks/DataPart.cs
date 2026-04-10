// ----------------------------------------------------------
//            文件：DataPart.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月24日 02:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Entities.VisualBlocks
{
    public sealed class DataPart : Nameable
    {
        /// <summary>
        /// 数据部件的类型。
        /// </summary>
        public string Type { get; init; }

        /// <summary>
        /// 数据部件实际的Base64编码后的数据。
        /// </summary>
        public string JSON { get; set; }

        /// <summary>
        /// 创建该数据部件的作者ID。
        /// </summary>
        public string OwnerID { get; init; }
    }
}