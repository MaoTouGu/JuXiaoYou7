using System.Reflection;

namespace MaoTouGu.Shells.AppConfigs
{
    public interface ILanguageOptionBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcid"></param>
        void SetLCID(string lcid);

        /// <summary>
        /// 使用某个文件夹。
        /// </summary>
        /// <param name="path"></param>
        void UseFolder(string path);
        void UseFile(string fileName);
        void UseAssembly(Assembly assembly);
        void UseProvider(ILanguageProvider provider);

        void UseLegacyAssembly<E>(Func<string, string> expression) where E : class;
        void UseLegacyFile(Func<string, object, string> expression);
        void UseLegacyFile<E>(Func<string, E, string> expression) where E : class;
    }
}