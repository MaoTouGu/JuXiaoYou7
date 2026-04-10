// ----------------------------------------------------------
//            文件：ICollectionPage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 23:56
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.AppModels
{
    public interface ICollectionPage<T>
    {
        T Selected { get; set; }

        ICommandEX Add    { get; }
        ICommandEX Edit   { get; }
        ICommandEX Remove { get; }

        ViewList<T> Collection { get; }
    }
}