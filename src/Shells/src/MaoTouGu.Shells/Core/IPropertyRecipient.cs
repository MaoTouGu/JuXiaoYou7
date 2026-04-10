// ----------------------------------------------------------
//            文件：IPropertyRecipient.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月24日 15:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Core
{
    public interface IPropertyRecipient
    {
        void SetValue(string name, object value);
    }
}