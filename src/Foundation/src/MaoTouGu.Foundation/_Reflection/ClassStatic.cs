using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace MaoTouGu.Foundation
{
    public static class ClassStatic
    {
        private static readonly Type[] EmptyParameters = Array.Empty<Type>();
        private static readonly ParameterModifier[] EmptyModifiers = Array.Empty<ParameterModifier>();
        
        /// <summary>
        /// 创建指定类型的实例。
        /// </summary>
        /// <typeparam name="T">指定要创建的实例类型。</typeparam>
        /// <returns>返回一个新创建的实例类型。</returns>
        public static T CreateInstance<T>() => TypeCache<T>.Activator();
        

        /// <summary>
        /// 缓存Lambda类型。
        /// </summary>
        /// <typeparam name="T">需要创建的实例类型。</typeparam>
        // ReSharper disable once ClassNeverInstantiated.Local
        private class TypeCache<T>
        {
            public static readonly Func<T> Activator = Classes<T>.CreateInstance(typeof(T));
        }

        #region GetEnumFieldAttribute

        /// <summary>
        /// 获取枚举值中的指定特性。
        /// </summary>
        /// <param name="instance">指定要获取的枚举。</param>
        /// <returns>返回获取的特性，如果不存在则返回null。</returns>
        public static string GetDescriptionAttribute(object instance)
        {
            if (instance is null)
            {
                return string.Empty;
            }

            if (instance is Enum @enum)
            {
                return GetDescriptionAttribute(@enum);
            }

            return instance.GetType()
                           .GetCustomAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute
                description
                ? description.Description
                : string.Empty;
        }

        /// <summary>
        /// 获取枚举值中的指定特性。
        /// </summary>
        /// <param name="instance">指定要获取的枚举。</param>
        /// <returns>返回获取的特性，如果不存在则返回null。</returns>
        public static string GetDescriptionAttribute(Enum instance) => GetEnumFieldAttribute<DescriptionAttribute>(instance)?.Description;

        /// <summary>
        /// 获取枚举值中的指定特性。
        /// </summary>
        /// <param name="instance">指定要获取的枚举。</param>
        /// <returns>返回获取的特性，如果不存在则返回null。</returns>
        public static T GetEnumFieldAttribute<T>(Enum instance) where T : Attribute
        {
            if (instance is null)
            {
                return default(T);
            }

            var field = instance.GetType().GetField(instance.ToString())!;

            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }

        /// <summary>
        /// 获取枚举值中的指定特性。
        /// </summary>
        /// <param name="type">指定要获取的枚举类型。</param>
        /// <param name="instance">指定要获取的枚举。</param>
        /// <returns>返回获取的特性，如果不存在则返回null。</returns>
        public static T GetEnumFieldAttribute<T>(Type type, Enum instance) where T : Attribute
        {
            if (instance is null)
            {
                return default(T);
            }

            var field = type.GetField(instance.ToString())!;

            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }

        /// <summary>
        /// 获取枚举值中的指定特性。
        /// </summary>
        /// <param name="instance">指定要获取的枚举。</param>
        /// <typeparam name="T">指定要获取的特性类型。</typeparam>
        /// <returns>返回获取的特性，如果不存在则返回null。</returns>
        public static T GetAttribute<T>(Enum instance)
        {
            if (instance is null)
            {
                return default(T);
            }

            return instance.GetType()
                           .GetCustomAttribute(typeof(T)) is T description
                ? description
                : default(T);
        }

        /// <summary>
        /// 获取枚举值中的指定特性。
        /// </summary>
        /// <param name="instance">指定要获取的枚举。</param>
        /// <param name="type">指定要获取的特性类型。</param>
        /// <returns>返回获取的特性，如果不存在则返回null。</returns>
        public static Attribute GetAttribute(Enum instance, Type type)
        {
            return instance?.GetType().GetCustomAttribute(type);
        }

        #endregion

        /// <summary>
        /// 寻找某个接口的所有实现类型。
        /// </summary>
        /// <param name="assembly">指定要检索的程序集，要求不能为空。</param>
        /// <returns>返回操作的结果，如果不存在则返回null。</returns>
        public static Type FindClassImplementation<T>(Assembly assembly)
        {
            return assembly?.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(T)));
        }

        #region FindInterfaceImplmentation

        /// <summary>
        /// 寻找某个接口的所有实现类型。
        /// </summary>
        /// <param name="assembly">指定要检索的程序集，要求不能为空。</param>
        /// <param name="interfaceType">指定要检索的接口类型，要求不能为空。</param>
        /// <returns>返回操作的结果，如果不存在则返回null。</returns>
        public static Type FindInterfaceImplementation(Assembly assembly, Type interfaceType)
        {
            if (assembly is null || interfaceType is null)
            {
                return null;
            }

            return assembly.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(interfaceType));
        }

        /// <summary>
        /// 寻找某个接口的所有实现类型。
        /// </summary>
        /// <param name="assembly">指定要检索的程序集，要求不能为空。</param>
        /// <returns>返回操作的结果，如果不存在则返回null。</returns>
        public static Type FindInterfaceImplementation<T>(Assembly assembly)
        {
            return assembly?.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(T)));
        }

        /// <summary>
        /// 寻找某个接口的所有实现类型。
        /// </summary>
        /// <param name="assembly">指定要检索的程序集，要求不能为空。</param>
        /// <returns>返回操作的结果，如果不存在则返回null。</returns>
        public static IEnumerable<Type> FindInterfaceImplementations<T>(Assembly assembly)
        {
            return assembly?.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(T)));
        }

        /// <summary>
        /// 寻找某个接口的所有实现类型。
        /// </summary>
        /// <param name="assembly">指定要检索的程序集，要求不能为空。</param>
        /// <param name="interfaceType">指定要检索的接口类型，要求不能为空。</param>
        /// <returns>返回操作的结果，如果不存在则返回null。</returns>
        public static IEnumerable<Type> FindInterfaceImplementations(Assembly assembly, Type interfaceType)
        {
            if (assembly is null || interfaceType is null)
            {
                return null;
            }

            return assembly.GetTypes().Where(x => x.GetInterfaces().Contains(interfaceType));
        }

        #endregion

        public static IEnumerable<T> GetEnums<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .ToArray();
        }
    }

    internal static class Classes<T>
    {
        public static Func<T> CreateInstance(Type type)
        {
            //
            // 为什么使用TypeInfo而不是Type
            // 
            //  https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo?view=net-5.0
            //
            // 通过浏览官方网站的定义我们发现TypeInfo主要用于Microsoft Store程序里面。
            Debug.Assert(type is not null);
            Debug.Assert(typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()));

            //
            // 找到构造函数中非静态构造函数并且无参数的构造函数。
            var constructor = type.GetTypeInfo()
                                  .DeclaredConstructors
                                  .FirstOrDefault(x => !x.IsStatic && !x.GetParameters().Any());

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            //
            // 使用Lambda表达式实现创建指定类型的实例。
            return System.Linq.Expressions.Expression.Lambda<Func<T>>(System.Linq.Expressions.Expression.New(constructor), null).Compile();
        }
    }
}