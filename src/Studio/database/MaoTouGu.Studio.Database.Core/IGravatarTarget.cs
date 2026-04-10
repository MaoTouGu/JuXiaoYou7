// ----------------------------------------------------------
//            文件：IGravatarTarget.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 17:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database
{
    public interface IGravatarTarget
    {
        /// <summary>
        /// 获得头像。
        /// </summary>
        /// <returns></returns>
        string GetGravatar();
        
        /// <summary>
        /// 设置头像。
        /// </summary>
        /// <param name="value"></param>
        void SetGravatar(string value);
    }
}