namespace MaoTouGu.Foundation.Collections
{
    public static class DictionaryExt
    {
        public static T SafetyGet<T>(this Dictionary<string, T> dictionary, string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            return dictionary.GetValueOrDefault(key);
        }
        
        public static T SafetyGet<T>(this IReadOnlyDictionary<string, T> dictionary, string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            return dictionary.GetValueOrDefault(key);
        }
        
        public static bool SafetyContains<T>(this Dictionary<string, T> dictionary, string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return dictionary.ContainsKey(key);
        }
        
        public static bool SafetyContains<T>(this IReadOnlyDictionary<string, T> dictionary, string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return dictionary.ContainsKey(key);
        }
    }
}