using System.Reflection;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CReflectionHelper
    {
        /// <summary>
        /// 从给定的程序集中获取派生自[TBase]且带有[TAttribute]特性的类型，并返回找到的第一个
        /// </summary>
        /// <param name="assembly"></param>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static Type Find<TBase, TAttribute>(Assembly assembly) where TAttribute : Attribute
        {
            var baseType = typeof(TBase);
            var attrType = typeof(TAttribute);

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface || type.IsEnum) continue;
                // 不是从TBase继承
                if (!baseType.IsAssignableFrom(type)) continue;

                var objects = type.GetCustomAttributes(attrType, false);
                if (objects.Length == 0) continue;

                return type;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static List<Type> FindAll<TBase, TAttribute>(Assembly assembly) where TAttribute : Attribute
        {
            var list = new List<Type>();

            var baseType = typeof(TBase);
            var attrType = typeof(TAttribute);

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface || type.IsEnum) continue;
                // 不是从TBase继承
                if (!baseType.IsAssignableFrom(type)) continue;

                var objects = type.GetCustomAttributes(attrType, false);
                if (objects.Length == 0) continue;

                list.Add(type);
            }

            return list;
        }

        /// <summary>
        /// 从当前的应用程序域获取派生自[TBase]且带有[TAttribute]特性的类型，并返回找到的第一个
        /// </summary>
        /// <typeparam name="TBase">基类，可以是interface</typeparam>
        /// <typeparam name="TAttribute">特性</typeparam>
        /// <returns></returns>
        public static Type Find<TBase, TAttribute>() where TAttribute : Attribute
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = Find<TBase, TAttribute>(assembly);
                if (type != null) return type;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static List<Type> FindAll<TBase, TAttribute>() where TAttribute : Attribute
        {
            var list = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = FindAll<TBase, TAttribute>(assembly);
                if (types.Count > 0) list.AddRange(types);
            }

            return list;
        }
    }
}