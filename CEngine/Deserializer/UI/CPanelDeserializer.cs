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
        private static Dictionary<string, IDeserializer> deserializers = new()
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
        public static CPanelView Deserialize(string file)
        {
            var data = CResources.LoadJson(file);
            return Deserialize(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static CPanelView Deserialize(JsonData data)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var width  = data.As("width", CScreen.Width);
            var height = data.As("height", CScreen.Height);
            var name = data.As("name", "panel");

            if (width  <= 0) width  = CScreen.Width;
            if (height <= 0) height = CScreen.Height;

            var view = new CPanelView(width, height, name);
            view.Transform.LocalPosition = new Vector2(x, y);

            DeserializeComponents(data["components"], view);

            return view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void DeserializeComponents(JsonData data, CPanelView view)
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
