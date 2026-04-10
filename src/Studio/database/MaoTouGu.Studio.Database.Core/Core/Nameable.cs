// ----------------------------------------------------------
//            文件：Nameable.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月17日 20:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    public abstract class Nameable : DatabaseObject
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