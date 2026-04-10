// ----------------------------------------------------------
//            文件：Ioc.GetOrRegister.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月19日 23:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using DryIoc;

namespace MaoTouGu.Shells
{
    partial class Ioc
    {

        public static T Register<T>() where T : class => Container.Resolve<T>();

        public static T GetOrRegister<T>() where T : class
        {
            if (Container.IsRegistered(typeof(T)))
            {
                return Get<T>();
            }
            
            Container.Register<T>();
            return Use<T>(Container.Resolve<T>());
        }

        public static T GetOrRegister<T, T1>() where T1 : notnull
                                               where T : class, T1
        {
            var r = GetOrRegister<T>();

            Use<T1, T>(r);

            return r;
        }

        public static T GetOrRegister<T, T1, T2>() where T1 : notnull
                                                   where T2 : notnull
                                                   where T : class, T1, T2
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T>(r);

            return r;
        }

        public static T GetOrRegister<T, T1, T2, T3>() where T1 : notnull
                                                       where T2 : notnull
                                                       where T3 : notnull
                                                       where T : class, T1, T2, T3
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T3, T>(r);

            return r;
        }

        public static T GetOrRegister<T, T1, T2, T3, T4>() where T1 : notnull
                                                           where T2 : notnull
                                                           where T3 : notnull
                                                           where T4 : notnull
                                                           where T : class, T1, T2, T3, T4
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T3, T4, T>(r);

            return r;
        }

        public static T GetOrRegister<T, T1, T2, T3, T4, T5>() where T1 : notnull
                                                               where T2 : notnull
                                                               where T3 : notnull
                                                               where T : class, T1, T2, T3, T4, T5
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T3, T>(r);

            return r;
        }

        public static T GetOrRegister<T, T1, T2, T3, T4, T5, T6>() where T1 : notnull
                                                                   where T2 : notnull
                                                                   where T3 : notnull
                                                                   where T4 : notnull
                                                                   where T5 : notnull
                                                                   where T6 : notnull
                                                                   where T : class, T1, T2, T3, T4, T5, T6
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T3, T4, T5, T6, T>(r);

            return r;
        }
        public static T GetOrRegister<T, T1, T2, T3, T4, T5, T6, T7>() where T1 : notnull
                                                                       where T2 : notnull
                                                                       where T3 : notnull
                                                                       where T4 : notnull
                                                                       where T5 : notnull
                                                                       where T6 : notnull
                                                                       where T7 : notnull
                                                                       where T : class, T1, T2, T3, T4, T5, T6, T7
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T3, T4, T5, T6, T7, T>(r);

            return r;
        }

        public static T GetOrRegister<T, T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : notnull
                                                                           where T2 : notnull
                                                                           where T3 : notnull
                                                                           where T4 : notnull
                                                                           where T5 : notnull
                                                                           where T6 : notnull
                                                                           where T7 : notnull
                                                                           where T8 : notnull
                                                                           where T : class, T1, T2, T3, T4, T5, T6, T7, T8
        {
            var r = GetOrRegister<T>();

            Use<T1, T2, T3, T4, T5, T6, T7, T8, T>(r);

            return r;
        }
    }
}