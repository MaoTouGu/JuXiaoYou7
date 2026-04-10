using Avalonia.Metadata;

[assembly: XmlnsDefinition("urn:MaoTouGu/ui", "MaoTouGu.Shells.AppModels")]
[assembly: XmlnsDefinition("urn:MaoTouGu/ui", "MaoTouGu.Shells.Controls")]

//
// 直接使用WPF默认namespace，就不需要打开头了。
[assembly: XmlnsDefinition("https://github.com/avaloniaui", "MaoTouGu.Shells.AppModels")]
[assembly: XmlnsDefinition("https://github.com/avaloniaui", "MaoTouGu.Shells.Controls")]