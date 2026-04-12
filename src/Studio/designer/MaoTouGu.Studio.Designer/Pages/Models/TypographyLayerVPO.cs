// ----------------------------------------------------------
//            文件：TypographyLayerVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 17:10
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    public class TypographyLayerVPO : ObservableObject
    {
        public bool IsLock
        {
            get => Layer.IsLock;
            set
            {
                Layer.IsLock = value;
                RaiseUpdated();
            }
        }

        public List<string> BlockIds => Layer.Blocks;
        
        public required List<TypographyBlockVPO> Blocks { get; init; }

        public TypographyLayer Layer { get; init; }
    }
}