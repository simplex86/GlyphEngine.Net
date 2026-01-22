using System;
using System.Collections.Generic;
using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// UI控件反序列化器
    /// </summary>
    internal static class CWidgetDeserializer
    {
        /// <summary>
        /// 默认的控件类型与反序列化器映射表
        /// </summary>
        private static Dictionary<string, IDeserializer<CWidget>> deserializers = new Dictionary<string, IDeserializer<CWidget>>()
        {
            { "text",        new CTextDeserializer() },
            { "image",       new CImageDeserializer() },
            { "button",      new CButtonDeserializer() },
            { "progressbar", new CProgressBarDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        static CWidgetDeserializer()
        {
            var types = CReflectionHelper.FindAll<IDeserializer<CWidget>, CWidgetDeserializerAttribute>();
            foreach (var type in types)
            {
                var wtype = GetWidgetType(type);
                deserializers[wtype] = Activator.CreateInstance(type) as IDeserializer<CWidget>;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contaner"></param>
        public static void Deserialize(JsonData data, IContainable<CWidget> contaner)
        {
            var type = data.As("type", string.Empty);
            if (deserializers.TryGetValue(type, out var deserializer))
            {
                deserializer.Deserialize(data, contaner);
            }
        }

        /// <summary>
        /// 获取特性中定义的控件类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetWidgetType(Type type)
        {
            var attr = Attribute.GetCustomAttribute(type, typeof(CWidgetDeserializerAttribute)) as CWidgetDeserializerAttribute;
            return attr.Type;
        }
    }

    /// <summary>
    /// UI控件特性
    /// </summary>
    internal class CWidgetDeserializerAttribute : Attribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public CWidgetDeserializerAttribute(string type)
        {
            Type = type;
        }
    }
}
