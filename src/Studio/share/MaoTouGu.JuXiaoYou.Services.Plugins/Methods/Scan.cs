// ----------------------------------------------------------
//            文件：Scan.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 20:35
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using System.Reflection;
using MaoTouGu.Foundation;
using MaoTouGu.Shells.Threadings;
using NLog;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public static partial class FeatureManager
    {
        private static IThreadingInvoker _Invoker;

        static bool FilterAssembly(Assembly assembly)
        {
            var name = assembly.FullName;

            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return !name.Contains("System")     &&
                   !name.Contains("Microsoft")  &&
                   !name.Contains("Newtonsoft") &&
                   !name.Contains("KinonekoSoftware");
        }

        static void InstallManifests(Assembly[] userAssemblies)
        {

            var collection = userAssemblies.Select(ClassStatic.FindInterfaceImplementations<IPluginManifest>)
                                           .SelectMany(x => x)
                                           .Where(x => !x.IsAbstract && !x.IsInterface)
                                           .Select(x =>
                                                   {
                                                       try
                                                       {
                                                           return (IPluginManifest)Activator.CreateInstance(x);
                                                       }
                                                       catch(Exception e)
                                                       {
                                                           _Logger.Info($"扫描{x.FullName}程序集时发生错误，无法创建IPluginManifest接口的实例。\n{e.Message}");
                                                           return null;
                                                       }
                                                   })
                                           .Where(x => x is not null)
                                           .ToList();
            _Logger.Info($"正在扫描所有程序集，在{userAssemblies.Length}个程序集中发现了{collection.Count}个实现了IPluginManifest接口的实例。");

            _Invoker ??= Ioc.Get<IThreadingInvoker>();
            
            InstallManifests(collection);
        }

        static void InstallManifests(ICollection<IPluginManifest> collection)
        {
            foreach (var manifest in collection)
            {
                _Invoker.RunOnUIThread(() =>
                                  {
                                      Manifests.Add(manifest);
                                  });
                
                //
                //
                manifest.RegisterFeatures();
                manifest.RegisterVisualManagers();
            }
        }

        public static void Scan()
        {
            var assemblies     = AppDomain.CurrentDomain.GetAssemblies();
            var userAssemblies = assemblies.Where(FilterAssembly).ToArray();

            InstallManifests(userAssemblies);
        }

        public static void Scan(string dir)
        {
            try
            {
                if (!Directory.Exists(DirectoryExt.GetOrCreate(dir)))
                {
                    _Logger.Info($"扫描{dir}目录的插件时发生错误，目录不存在。");
                    return;
                }


                var maybeAssemblyFiles = Directory.GetFiles(dir, "*.dll");

                _Logger.Info($"扫描{dir}目录时发现{maybeAssemblyFiles.Length}个可能为插件的文件。");

                var assemblies = maybeAssemblyFiles.Select(x =>
                                                           {
                                                               try
                                                               {
                                                                   return Assembly.LoadFile(x);
                                                               }
                                                               catch(Exception e)
                                                               {
                                                                   return null;
                                                               }
                                                           });

                var userAssemblies = assemblies.Where(x => x is not null)
                                               .Where(FilterAssembly)
                                               .ToArray();

                InstallManifests(userAssemblies);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}