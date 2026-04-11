// ----------------------------------------------------------
//            文件：Moniker.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月17日 20:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.Foundation.Collections;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.Studio.Database.Objects
{
    [DebuggerDisplay("Name = {Name}, Id = {Id}")]
    public sealed class Moniker : Authorable, IGravatarTarget
    {
        private string _name;
        private string _gravatar;

        public static Moniker Create(string name, User user)
        {
            var time = DateTime.Now;

            return new Moniker
            {
                Settings = new MonikerSettingSet(),
                Id          = ID.Get(),
                Name        = name,
                Created     = time,
                Modified    = time,
                Creator     = user?.Id,
                CreatorName = user?.DisplayName,
            };
        }

        public static Moniker Create(string id, string name, User user)
        {
            var time = DateTime.Now;

            return new Moniker
            {
                Settings = new MonikerSettingSet(),
                Id          = id,
                Name        = name,
                Created     = time,
                Modified    = time,
                Creator     = user?.Id,
                CreatorName = user?.DisplayName,
            };
        }

        public bool ContainSettingItem(string key) => Settings is not null && Settings.SafetyContains(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetSetting(string key, out string value)
        {
            if (Settings is not null && Settings.TryGetValue(key, out value))
            {
                return true;
            }

            value = null;
            return false;
        }

        public string SafetyGetSetting(string key)
        {
            if (Settings is not null && Settings.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool AsBoolean(string key)
        {
            if(bool.TryParse(SafetyGetSetting(key), out var result))
            {
                return result;
            }

            return false;
        }

        public void Set(string key, int value) => Set(key, value.ToString());
        public void Set(string key, float value) => Set(key, value.ToString("F5"));
        public void Set(string key, DateTime value) => Set(key, value.Ticks.ToString());
        public void Set(string key, bool value) => Set(key, value.ToString());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, string value)
        {
            if (Settings is null)
            {
                return;
            }

            if (Settings.ContainsKey(key))
            {
                Settings[key] = value;
            }
            else
            {
                Settings.TryAdd(key, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string AsString(string key)
        {
            if (Settings is not null && !string.IsNullOrEmpty(key) && Settings.TryGetValue(key, out var v))
            {
                return v;
            }

            return string.Empty;
        }

        public string GetGravatar() => Gravatar;
        
        public void SetGravatar(string value)
        {
            Gravatar = value;
        }
        
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Gravatar
        {
            get => AsString(nameof(Gravatar));
            set
            {
                Set(nameof(Gravatar), value);
                RaiseUpdated();
            }
        }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Name
        {
            get => AsString(nameof(Name));
            set
            {
                Set(nameof(Name), value);
                RaiseUpdated(nameof(FriendlyName));
            }
        }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string FriendlyName => string.IsNullOrEmpty(Name) ? "（空）" : Name;

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsStar
        {
            get => AsBoolean(nameof(IsStar));
            set
            {
                Set(nameof(IsStar), value);
                RaiseUpdated();
            }
        }

        public DateTime Modified { get; set; }
        public DateTime Created  { get; init; }

        public MonikerSettingSet Settings { get; init; }
    }
}