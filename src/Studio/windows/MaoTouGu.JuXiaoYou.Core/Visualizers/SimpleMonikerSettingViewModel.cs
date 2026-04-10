// ----------------------------------------------------------
//            文件：SimpleMonikerSettingViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 02:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public class SimpleMonikerSettingViewModel : InstancePage
    {
        public SimpleMonikerSettingViewModel(Moniker moniker)
        {
            Moniker = moniker;
        }
        
        public Moniker Moniker { get; }
    }
}