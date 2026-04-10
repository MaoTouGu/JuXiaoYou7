using System.Text;
using Newtonsoft.Json;

namespace MaoTouGu.Shells.Core
{
    public static class JSON
    {
        private static readonly JsonSerializerSettings TypeHandler = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        
        private static readonly JsonSerializerSettings NonTypeHandler = new JsonSerializerSettings();


        public static T Deserialize<T>(string text) => JsonConvert.DeserializeObject<T>(text, NonTypeHandler);
        
        public static string Serialize<T>(T instance) => JsonConvert.SerializeObject(instance, NonTypeHandler);


        public static T FromFile<T>(string path) where T : class, new()
        {
            try
            {
                var payload = File.ReadAllText(path);
                return Deserialize<T>(payload);
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
                try
                {
                    var payload = File.ReadAllText(path);
                    instance = Deserialize<T>(payload);
                }
                catch
                {
                    instance = factory();
                    ToFile<T>(path, instance);
                }
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
            var payload = Serialize(setting);
            File.WriteAllText(path, payload);
        }

        public static string ToBase64(string payload)
        {
            var buffer = Encoding.UTF8.GetBytes(payload);
            return Convert.ToBase64String(buffer);
        }

        public static T FromBase64<T>(string payload) where T : class, new()
        {
            var buffer = Convert.FromBase64String(payload);
            var json = Encoding.UTF8.GetString(buffer);

            return Deserialize<T>(json);
        }
        
        
    }


    public static class JSON2
    {
        
        private static readonly JsonSerializerSettings TypeHandler = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };

        public static T Deserialize<T>(string text) => JsonConvert.DeserializeObject<T>(text, TypeHandler);
        
        public static string Serialize<T>(T instance) => JsonConvert.SerializeObject(instance, TypeHandler);
        
        
        public static T FromFile<T>(string path) where T : class
        {
            try
            {
                var payload = File.ReadAllText(path);
                return Deserialize<T>(payload);
            }
            catch
            {
                return null;
            }
        }
        
        public static T FromFile<T>(string path, Func<T> factory)  where T : class
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            T instance;
            
            if (File.Exists(path))
            {
                try
                {
                    var payload = File.ReadAllText(path);
                    instance = Deserialize<T>(payload);
                }
                catch
                {
                    instance = factory();
                    ToFile<T>(path, instance);
                }
            }
            else
            {
                instance = factory();
                ToFile<T>(path, instance);
            }


            return instance;
        }
        
        public static void ToFile<T>(string path, T setting) where T : class
        {
            var payload = Serialize(setting);
            File.WriteAllText(path, payload);
        }
        
        public static string ToBase64<T>(T instance) where T : class
        {
            var payload = Serialize(instance);
            var buffer  = Encoding.UTF8.GetBytes(payload);
            return Convert.ToBase64String(buffer);
        }

        public static T FromBase64<T>(string payload) where T : class
        {
            var buffer = Convert.FromBase64String(payload);
            var json   = Encoding.UTF8.GetString(buffer);

            return Deserialize<T>(json);
        }
    }
}