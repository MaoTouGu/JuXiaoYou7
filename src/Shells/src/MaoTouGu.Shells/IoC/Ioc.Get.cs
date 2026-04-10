using DryIoc;

namespace MaoTouGu.Shells
{
    partial class Ioc
    {
        /// <summary>
        /// 获取指定类型的服务。
        /// </summary>
        /// <typeparam name="T">指定的服务类型，必须是接口、类、委托、数组</typeparam>
        /// <remarks>Of 方法可以通过内建的IoC获得指定的服务。</remarks>
        /// <returns>返回一个指定的服务类型，</returns>
        public static T Get<T>() where T : class => Container.Resolve<T>();

        public static T SafeGet<T>() where T : class
        {
            if (Container.IsRegistered<T>())
            {
                return Container.Resolve<T>();
            }

            return default(T);
        }

        
        public static void Get<T1, T2>(out T1 r1, out T2 r2)
            where T1 : class
            where T2 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
        }
        
        
        public static void Get<T1, T2, T3>(out T1 r1, out T2 r2,out T3 r3)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
            r3 = container.Resolve<T3>();
        }
        
        public static void Get<T1, T2, T3, T4>(out T1 r1, out T2 r2,out T3 r3, out T4 r4)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
            r3 = container.Resolve<T3>();
            r4 = container.Resolve<T4>();
        }
        
        public static void Get<T1, T2, T3, T4, T5>(out T1 r1, out T2 r2,out T3 r3, out T4 r4,out T5 r5)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
            r3 = container.Resolve<T3>();
            r4 = container.Resolve<T4>();
            r5 = container.Resolve<T5>();
        }
        
        public static void Get<T1, T2, T3, T4, T5, T6>(out T1 r1, out T2 r2,out T3 r3, out T4 r4,out T5 r5, out T6 r6)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
            r3 = container.Resolve<T3>();
            r4 = container.Resolve<T4>();
            r5 = container.Resolve<T5>();
            r6 = container.Resolve<T6>();
        }
        
        public static void Get<T1, T2, T3, T4, T5, T6, T7>(out T1 r1, out T2 r2,out T3 r3, out T4 r4,out T5 r5, out T6 r6, out T7 r7)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
            r3 = container.Resolve<T3>();
            r4 = container.Resolve<T4>();
            r5 = container.Resolve<T5>();
            r6 = container.Resolve<T6>();
            r7 = container.Resolve<T7>();
        }

        public static void Get<T1, T2, T3, T4, T5, T6, T7, T8>(out T1 r1, out T2 r2,out T3 r3, out T4 r4,out T5 r5, out T6 r6, out T7 r7, out T8 r8)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
        {
            var container = Container;
            r1 = container.Resolve<T1>();
            r2 = container.Resolve<T2>();
            r3 = container.Resolve<T3>();
            r4 = container.Resolve<T4>();
            r5 = container.Resolve<T5>();
            r6 = container.Resolve<T6>();
            r7 = container.Resolve<T7>();
            r8 = container.Resolve<T8>();
        }
        

        /// <summary>
        /// 获得指定服务
        /// </summary>
        /// <typeparam name="T">指定的服务类型</typeparam>
        /// <param name="type">指定要获得的类型。</param>
        /// <returns>返回指定的服务</returns>
        public static T Get<T>(Type type) => (T)Container.Resolve(type);
        

        /// <summary>
        /// 判断是否已经注册。
        /// </summary>
        /// <param name="type">指定要判断的类型。</param>
        /// <returns>已经注册将会返回true，否则返回false。</returns>
        public static bool IsRegistered(Type type) => type is not null && Container.IsRegistered(type);
        
        /// <summary>
        /// 判断是否已经注册。
        /// </summary>
        /// <typeparam name="T">指定要判断的类型。</typeparam>
        /// <returns>已经注册将会返回true，否则返回false。</returns>
        public static bool IsRegistered<T>() => Container.IsRegistered<T>();
    }
}