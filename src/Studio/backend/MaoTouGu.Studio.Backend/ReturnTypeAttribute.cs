// ----------------------------------------------------------
//            文件：ReturnTypeAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 15:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio
{
    [AttributeUsage(AttributeTargets.ReturnValue)]
    public class ReturnTypeAttribute<T> : Attribute
    {
        
    }

    [AttributeUsage(AttributeTargets.ReturnValue)]
    public class GzipReturnAttribute : Attribute
    {
        
    }
}