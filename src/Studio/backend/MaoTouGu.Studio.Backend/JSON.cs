namespace MaoTouGu.Shells.Core
{
    internal static class JSON
    {
        private static readonly JsonSerializerSettings TypeHandler = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        
        private static readonly JsonSerializerSettings NonTypeHandler = new JsonSerializerSettings();


        public static T Get<T>(string text) => JsonConvert.DeserializeObject<T>(text, NonTypeHandler);
        public static string Set<T>(T instance) => JsonConvert.SerializeObject(instance, NonTypeHandler);


        public static T FromFile<T>(string path) where T : class, new()
        {
            try
            {
                var payload = File.ReadAllText(path);
                return Get<T>(payload);
            }
            catch
            {
                return null;
            }
        }
        
        public static T FromFile<T>(string path, Func<T> factory)  where T : class, new()
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            T instance;
            
            if (File.Exists(path))
            {
                var payload = File.ReadAllText(path);
                instance = Get<T>(payload);
            }
            else
            {
                instance = factory();
                ToFile<T>(path, instance);
            }


            return instance;
        }
        
        public static void ToFile<T>(string path, T setting) where T : class, new()
        {
            var payload = Set(setting);
            File.WriteAllText(path, payload);
        }
    }
}