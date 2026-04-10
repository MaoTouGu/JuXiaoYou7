using System.IO;
using ImageLRUTable = System.Collections.Concurrent.ConcurrentDictionary<string, System.WeakReference<System.Windows.Media.Imaging.BitmapImage>>;

namespace MaoTouGu.JuXiaoYou
{
    public static partial class AppExt
    {

        public static void UseJuXiaoYouText(this ILanguageOptionBuilder provider)
        {
            provider.UseLegacyAssembly<JuXiaoYouPage>(lcid => $"MaoTouGu.JuXiaoYou.Languages.{lcid}.txt");
            provider.UseLegacyAssembly<JuXiaoYouPage>(lcid => $"MaoTouGu.JuXiaoYou.Languages.Enum_{lcid}.txt");
        }
    }
}