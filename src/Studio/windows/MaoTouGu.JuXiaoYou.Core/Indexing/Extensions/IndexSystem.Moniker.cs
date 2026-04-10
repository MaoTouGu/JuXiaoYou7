// ----------------------------------------------------------
//            文件：IndexSystem.Moniker.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 20:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Indexing
{
    partial class IndexSystem
    {
        



        public static async Task Update<T>(T target) where T : DatabaseObject
        {
            var m = MonikerService.Collection.FirstOrDefault(x => x.Id == target.Id);

            if (m is null)
            {
                return;
            }

            var oldName     = m.Name;
            var oldGravatar = m.Gravatar;
            var change      = false;

            if (target is Nameable n && n.Name != oldName)
            {
                m.Name = n.Name;
                change = true;
            }

            if (target is IGravatarTarget iGT && iGT.GetGravatar() != oldGravatar)
            {
                m.Gravatar = iGT.GetGravatar();
                change     = true;
            }

            if (change)
            {
                await MonikerService.Update(m);
            }

        }

        public static async Task RemoveMoniker(DatabaseObject target)
        {
            var r = MonikerService.Collection.FirstOrDefault(x => x.Id == target.Id);

            if (r is null)
            {
                return;
            }

            r.Modified      = DateTime.Now;
            r.IsSoftDeleted = true;

            await MonikerService.Update(r);
        }

        public static async Task<Moniker> AddMoniker<T>(T instance, Action<T, Moniker> gravatarSelector = null) where T : DatabaseObject
        {
            var moniker = Moniker.Create(instance.Id, (instance as Nameable)?.Name, GlobalSettings.User);

            if (instance is Nameable n)
            {
                moniker.Name = n.Name;
            }

            if (instance is IGravatarTarget igt)
            {
                moniker.Gravatar = igt.GetGravatar();
            }

            gravatarSelector?.Invoke(instance, moniker);

            await MonikerService.Add(moniker);

            return moniker;
        }
    }
}