// ----------------------------------------------------------
//            文件：MonikerSettingSet.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月17日 20:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation.Collections;

namespace MaoTouGu.Studio.Database.Core
{
    public sealed class MonikerSettingSet : ViewTable<string, string>
    {
        //
        // Visual.*.<VisualManagerID> = 视觉对象ID
        // 例如：Visual.Card.94D14D0770814579919562518D58239D = 9CD45DEFD4AE4D3DA9F663761A92DBAF
        
        //
        // Reference.* = 具体的筛选方式
    }
}