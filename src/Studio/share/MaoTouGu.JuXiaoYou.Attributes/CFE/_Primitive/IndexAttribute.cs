// ----------------------------------------------------------
//            文件：IndexAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IndexAttribute : CFAttribute
    {
        public IndexAttribute(int index)
        {
            Index = index;
        }

        public int Index { get; init; }
    }
}