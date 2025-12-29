using System;
using System.Collections.Generic;
using LitJson;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CPanelDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, IDeserializer<CWidget>> deserializers = new()
        {
            { "text", new CTextDeserializer() },
            { "button", new CButtonDeserializer() },
            { "image", new CImageDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static CPanel Deserialize(string file, Type type)
        {
            var data = CResources.LoadJson(file);
            return Deserialize(data, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static CPanel Deserialize(JsonData data, Type type)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var width  = data.As("width", CScreen.Width);
            var height = data.As("height", CScreen.Height);
            var name = data.As("name", "panel");

            if (width  <= 0) width  = CScreen.Width;
            if (height <= 0) height = CScreen.Height;

            var panel = Activator.CreateInstance(type) as CPanel;
            panel.Width = width;
            panel.Height = height;
            panel.Name = name;
            panel.Transform.LocalPosition = new Vector2(x, y);

            DeserializeComponents(data["components"], panel);

            return panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void DeserializeComponents(JsonData data, IContainable<CWidget> view)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].TryGetValue("type", out var type) &&
                    deserializers.TryGetValue((string)type, out var deserializer))
                {
                    deserializer.Deserialize(data[i], view);
                }
            }
        }
    }
}
