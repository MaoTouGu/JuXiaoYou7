using DryIoc;

namespace MaoTouGu.Shells
{
    partial class Ioc
    {
        #region UseInstance

        public static void UseInstance<T1, T2>(object instance)
        {
            if (instance is T1 _instance1)
            {
                Container
                    .RegisterInstance(
                        typeof(T1),
                        _instance1,
                        IfAlreadyRegistered.Replace);
            }

            if (instance is T2 _instance2)
            {
                Container
                    .RegisterInstance(
                        typeof(T2),
                        _instance2,
                        IfAlreadyRegistered.Replace);
            }
        }

        public static void UseInstance<T1, T2, T3>(object instance)
        {
            if (instance is T1 _instance1)
            {
                Container.RegisterInstance(
                    typeof(T1),
                    _instance1,
                    IfAlreadyRegistered.Replace);
            }

            if (instance is T2 _instance2)
            {
                Container.RegisterInstance(
                    typeof(T2),
                    _instance2,
                    IfAlreadyRegistered.Replace);
            }

            if (instance is T3 _instance3)
            {
                Container.RegisterInstance(
                    typeof(T3),
                    _instance3,
                    IfAlreadyRegistered.Replace);
            }
        }

        public static void UseInstance<T1, T2, T3, T4>(object instance)
        {
            if (instance is T1)
            {
                Container.RegisterInstance(
                    typeof(T1),
                    instance,
                    IfAlreadyRegistered.Replace);
            }

            if (instance is T2)
            {
                Container.RegisterInstance(
                    typeof(T2),
                    instance,
                    IfAlreadyRegistered.Replace);
            }

            if (instance is T3)
            {
                Container.RegisterInstance(
                    typeof(T3),
                    instance,
                    IfAlreadyRegistered.Replace);
            }
            
            if (instance is T3)
            {
                Container.RegisterInstance(
                    typeof(T4),
                    instance,
                    IfAlreadyRegistered.Replace);
            }
        }

        #endregion
        
        #region Use

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T>(T instance) where T : class
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T>(T instance)
            where T1 : notnull
            where T : class, T1
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T>(T instance) where T1 : notnull
                                                   where T2 : notnull
                                                   where T : class, T1, T2
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <typeparam name="T3">接口3</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T3, T>(T instance)
            where T1 : notnull
            where T2 : notnull
            where T3 : notnull
            where T : class, T1, T2, T3
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T3), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <typeparam name="T3">接口3</typeparam>
        /// <typeparam name="T4">接口4</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T3, T4, T>(T instance)
            where T1 : notnull
            where T2 : notnull
            where T3 : notnull
            where T4 : notnull
            where T : class, T1, T2, T3, T4
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T3), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T4), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <typeparam name="T3">接口3</typeparam>
        /// <typeparam name="T4">接口4</typeparam>
        /// <typeparam name="T5">接口5</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T3, T4, T5, T>(T instance)
            where T1 : notnull
            where T2 : notnull
            where T3 : notnull
            where T4 : notnull
            where T5 : notnull
            where T : class, T1, T2, T3, T4, T5
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T3), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T4), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T5), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <typeparam name="T3">接口3</typeparam>
        /// <typeparam name="T4">接口4</typeparam>
        /// <typeparam name="T5">接口5</typeparam>
        /// <typeparam name="T6">接口6</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T3, T4, T5, T6, T>(T instance)
            where T1 : notnull
            where T2 : notnull
            where T3 : notnull
            where T4 : notnull
            where T5 : notnull
            where T6 : notnull
            where T : class, T1, T2, T3, T4, T5, T6
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T3), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T4), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T5), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T6), instance, IfAlreadyRegistered.Replace);

            return instance;
        }


        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <typeparam name="T3">接口3</typeparam>
        /// <typeparam name="T4">接口4</typeparam>
        /// <typeparam name="T5">接口5</typeparam>
        /// <typeparam name="T6">接口6</typeparam>
        /// <typeparam name="T7">接口7</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T3, T4, T5, T6, T7, T>(T instance)
            where T1 : notnull
            where T2 : notnull
            where T3 : notnull
            where T4 : notnull
            where T5 : notnull
            where T6 : notnull
            where T7 : notnull
            where T : class, T1, T2, T3, T4, T5, T6, T7
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T3), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T4), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T5), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T6), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T7), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        /// <summary>
        /// 注册实例。
        /// </summary>
        /// <param name="instance">要注册的实例</param>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <typeparam name="T1">接口1</typeparam>
        /// <typeparam name="T2">接口2</typeparam>
        /// <typeparam name="T3">接口3</typeparam>
        /// <typeparam name="T4">接口4</typeparam>
        /// <typeparam name="T5">接口5</typeparam>
        /// <typeparam name="T6">接口6</typeparam>
        /// <typeparam name="T7">接口7</typeparam>
        /// <typeparam name="T8">接口8</typeparam>
        /// <returns>返回实例。</returns>
        public static T Use<T1, T2, T3, T4, T5, T6, T7, T8, T>(T instance)
            where T1 : notnull
            where T2 : notnull
            where T3 : notnull
            where T4 : notnull
            where T5 : notnull
            where T6 : notnull
            where T7 : notnull
            where T8 : notnull
            where T : class, T1, T2, T3, T4, T5, T6, T7, T8
        {
            if (instance is null)
            {
                return default(T);
            }

            Container.RegisterInstance(instance);
            Container.RegisterInstance(typeof(T1), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T2), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T3), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T4), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T5), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T6), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T7), instance, IfAlreadyRegistered.Replace);
            Container.RegisterInstance(typeof(T8), instance, IfAlreadyRegistered.Replace);

            return instance;
        }

        #endregion
    }
}