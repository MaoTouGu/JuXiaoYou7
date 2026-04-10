using MaoTouGu.Shells.Attributes;

namespace MaoTouGu.Shells
{
    [I18N]
    public enum ButtonText
    {
        [LocalizedString("zh-CN", "确认")]
        [LocalizedString("en-US", "Ok")]
        Ok,
        
        [LocalizedString("zh-CN", "取消")]
        [LocalizedString("en-US", "Cancel")]
        Cancel,
        
        Yes,
        
        NextStep,
    }
}