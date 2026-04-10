// ----------------------------------------------------------
//            文件：Visualization.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月17日 20:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Objects
{
    /// <summary>
    /// 可视化
    /// </summary>
    public abstract class Visualization : Authorable
    {
        private string _name;

        /// <summary>
        /// 获取或设置 <see cref="Name"/> 属性。
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
    }
}