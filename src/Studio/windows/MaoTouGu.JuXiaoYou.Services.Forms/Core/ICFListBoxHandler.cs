// ----------------------------------------------------------
//            文件：ICFListBoxHandler.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:49
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public interface ICFListBoxHandler
    {
        /// <summary>
        /// 所有数据。
        /// </summary>
        IEnumerable<object> ItemsSource { get; }
        
        /// <summary>
        /// 值选择器。
        /// </summary>
        /// <remarks>
        /// 在实际过程中，我们可能选择的是一个对象，但是需要返回的是一个属性。
        /// </remarks>
        CFListBoxValueSelector ValueSelector { get; }
        
        /// <summary>
        /// 值选择器。
        /// </summary>
        /// <remarks>
        /// 在实际过程中，我们可能选择的是一个属性，但是需要返回的是一个对象。
        /// </remarks>
        CFListBoxObjectSelector ObjectSelector { get; }
        
        /// <summary>
        /// 获得Template。
        /// </summary>
        DataTemplate Template { get; }
    }
}