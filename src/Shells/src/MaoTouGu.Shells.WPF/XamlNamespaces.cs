using System.Windows.Markup;

[assembly: XmlnsDefinition("urn:MaoTouGu/ui", "MaoTouGu.Shells.AppModels")]
[assembly: XmlnsDefinition("urn:MaoTouGu/ui", "MaoTouGu.Shells.Controls")]

//
// 直接使用WPF默认namespace，就不需要打开头了。
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "MaoTouGu.Shells")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "MaoTouGu.Shells.AppModels")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "MaoTouGu.Shells.Controls")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "MaoTouGu.Shells.Converters")]