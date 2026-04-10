// ----------------------------------------------------------
//            文件：ICompositableForm.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    //
    // 比如Feature是一个Poco类，我需要一个专为它提供编辑器界面的服务。
    // 
    
    public interface ICompositableForm
    {
        // /// <summary>
        // /// 验证当前元素是否完成。
        // /// </summary>
        // /// <param name="elements"></param>
        // /// <returns></returns>
        // bool Verify(CFElementCollection elements);
        //
        // /// <summary>
        // /// 验证选项是否成功。
        // /// </summary>
        // /// <param name="element"></param>
        // /// <returns></returns>
        // bool Verify(CFElement element);
        //
        
        /// <summary>
        /// 获得指定列表元素的处理器。
        /// </summary>
        /// <param name="type">要编辑的类型</param>
        /// <param name="propertyName">属性</param>
        /// <returns></returns>
        ICFListBoxHandler GetHandler(Type type, string propertyName);

        /// <summary>
        /// 获得上下文编辑器。
        /// </summary>
        /// <param name="type">要编辑的类型</param>
        /// <param name="propertyName">属性</param>
        /// <returns></returns>
        Task GetObjectContext(Type type, string propertyName);

        void TryFinish();
    }
}