using System.Reflection;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CReflectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static Type Find<TBase>(Assembly assembly, string typename)
        {
            var baseType = typeof(TBase);

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface || type.IsEnum) continue;
                if (!baseType.IsAssignableFrom(type)) continue;// 不是从TBase继承
                if (type.FullName == typename) return type;
            }

            return null;
        }

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
                if (!baseType.IsAssignableFrom(type)) continue;// 不是从TBase继承

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
                if (!baseType.IsAssignableFrom(type)) continue;// 不是从TBase继承

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typename"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(string typename)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = Find<T>(assembly, typename);
                if (type != null) return (T)Activator.CreateInstance(type);
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="typename"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstance<T, A>(string typename, A args)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = Find<T>(assembly, typename);
                if (type != null) return (T)Activator.CreateInstance(type, args);
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T BitwiseClone<T>(T target)
        {
            if (target == null)
            {
                return target;
            }

            var type = target.GetType();
            // 字符串 或 值类型
            if (target is string || type.IsValueType)
            {
                return target;
            }

            var clone = Activator.CreateInstance(type);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(clone, field.GetValue(target));
                }
                catch
                {
                }
            }

            return (T)clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T BitwiseClone<T, A>(T target, A args)
        {
            if (target == null)
            {
                return target;
            }

            var type = target.GetType();
            // 字符串 或 值类型
            if (target is string || type.IsValueType)
            {
                return target;
            }

            var clone = Activator.CreateInstance(type, args);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(clone, field.GetValue(target));
                }
                catch
                {
                }
            }

            return (T)clone;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T MutableClone<T>(T target)
        {
            if (target == null)
            {
                return target;
            }

            var type = target.GetType();
            // 字符串 或 值类型
            if (target is string || type.IsValueType)
            {
                return target;
            }

            var clone = Activator.CreateInstance(type);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(clone, MutableClone(field.GetValue(target)));
                }
                catch 
                { 
                }
            }

            return (T)clone;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T MutableClone<T, A>(T target, A args)
        {
            if (target == null)
            {
                return target;
            }

            var type = target.GetType();
            // 字符串 或 值类型
            if (target is string || type.IsValueType)
            {
                return target;
            }

            var clone = Activator.CreateInstance(type, args);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(clone, MutableClone(field.GetValue(target)));
                }
                catch
                {
                }
            }

            return (T)clone;
        }
    }
}