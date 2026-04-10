namespace MaoTouGu.Shells.Languages
{
    public static class I18N
    {
        private static readonly Dictionary<string, string> _InternalText = new Dictionary<string, string>();

        internal const uint Header = 0x1F2E3D4C;

        //
        // I18N的重构将会改进很多与IO和编码相关的内容。
        // 将会利用源代码生成器将将会以.i18n文件为

        #region Install

        public static void SetLanguage(ILanguageProvider provider)
        {
            if (provider is null)
            {
                return;
            }

            using (provider)
            {
                provider.Provide(_InternalText);
            }
        }

        #endregion

        #region GetText

        
        public static string GetText(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            
            return _InternalText.GetValueOrDefault(key, key);
        }

        public static string GetEnum(Enum @enum)
        {
            return GetText($"Enum.{@enum.GetType().Name}.{@enum}");
        }

        public static string GetEnum<T>(T @enum) where T : Enum
        {
            return GetText($"Enum.{@enum.GetType().Name}.{@enum}");
        }

        public static string GetViewModel(object instance)
        {
            return GetText($"App.{instance.GetType().Name}");
        }

        #endregion
        
        public static string LCID { get; internal set; }
    }
}