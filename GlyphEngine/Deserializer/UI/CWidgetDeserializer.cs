using System;
using System.Collections.Generic;
using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CWidgetDeserializer
    {
        private static Dictionary<EWidgetType, IDeserializer<CWidget>> deserializers = new Dictionary<EWidgetType, IDeserializer<CWidget>>()
        {
            { EWidgetType.Text,        new CTextDeserializer() },
            { EWidgetType.Image,       new CImageDeserializer() },
            { EWidgetType.Button,      new CButtonDeserializer() },
            { EWidgetType.ProgressBar, new CProgressBarDeserializer() },
        };

        public static void Deserialize(JsonData data, IContainable<CWidget> contaner)
        {
            var type = data.As("type", EWidgetType.None);
            if (deserializers.TryGetValue(type, out var deserializer))
            {
                deserializer.Deserialize(data, contaner);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class CWidgetDeserializerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public EWidgetType Type { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public CWidgetDeserializerAttribute(EWidgetType type)
        {
            Type = type;
        }
    }
}
