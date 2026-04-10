using System.Diagnostics;
using System.Reflection;
using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.AppConfigs
{
    sealed class LanguageOptionBuilderImpl<T>(AppConfigBuilderImpl<T> _impl) : ILanguageOptionBuilder where T : class, new()
    {
        private List<ILanguageProvider> _collection;

        private string _lcid;
        
        public void SetLCID(string lcid)
        {
            _lcid = lcid;
            _impl.SetLCID(lcid);
        }
        
        public void UseFolder(string path)
        {
            var path2 = Path.Combine(path, $"{_lcid}.i18n");

            if (!File.Exists(path2))
            {
                return;
            }
            
            UseFile(path2);
        }
        
        public void UseFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return;
                }
                
                using var stream    = File.OpenRead(fileName);
                using var i18Reader = new I18NReader(stream);

                _collection.Add(new LocalFileProvider(fileName));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        
        public void UseAssembly(Assembly assembly)
        {
            if (assembly is null)
            {
                return;
            }
            
            try
            {
                var impls = ClassStatic.FindInterfaceImplementations<ILanguageProvider>(assembly)
                                       .Select(Activator.CreateInstance)
                                       .Cast<ILanguageProvider>();
                _collection.AddRange(impls);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        
        public void UseProvider(ILanguageProvider provider)
        {
            if (provider is null)
            {
                return;
            }
            
            _collection.Add(provider);
        }
        
        public void UseLegacyAssembly<E>(Func<string, string> expression) where E : class
        {
            if (expression is null)
            {
                return;
            }

            var manifestName  = expression(_lcid);
            var assembly      = typeof(E).Assembly;
            var manifestSteam = assembly.GetManifestResourceStream(manifestName);

            if (manifestSteam is null)
            {
                return;
            }
            
            _collection.Add(new LegacyLanguageProvider(manifestSteam));
        }
        
        public void UseLegacyFile(Func<string, object, string> expression)
        {
            if (expression is null)
            {
                return;
            }

            var setting  = _impl.Setting;
            var fileName = expression(_lcid, setting);

            if (!File.Exists(fileName))
            {
                return;
            }

            var stream = File.OpenRead(fileName);
            _collection.Add(new LegacyLanguageProvider(stream));
        }     
        
        public void UseLegacyFile<E>(Func<string, E, string> expression) where E : class
        {
            if (expression is null)
            {
                return;
            }
            

            var setting  = _impl.Setting;
            var fileName = expression(_lcid, setting as E);

            if (!File.Exists(fileName))
            {
                return;
            }

            var stream = File.OpenRead(fileName);
            _collection.Add(new LegacyLanguageProvider(stream));
        }


        internal void SetCollection(List<ILanguageProvider> collection)
        {
            _collection = collection;
        }
    }
}