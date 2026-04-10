using MaoTouGu.Shells.AppConfigs;

namespace MaoTouGu.Shells.Languages
{
    public static class BuiltinLanguageExtensions
    {
        public static void UseShellText(this ILanguageOptionBuilder provider)
        {
            provider.UseLegacyAssembly<AssemblyFileProvider>(lcid => $"MaoTouGu.Shells.Languages.Enum_{lcid}.txt");
        }
    }
}