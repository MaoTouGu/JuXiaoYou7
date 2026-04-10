// ----------------------------------------------------------
//            文件：CFListBoxValueSelector.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public delegate object CFListBoxValueSelector(object item);

    public delegate object CFListBoxObjectSelector(IEnumerable<object> collection, object property);
}