using System.Collections.Generic;
using LitJson;

namespace CEngine.UI
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
            var data = CResourceManager.LoadJson(file);
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
            var width  = data.As("width", CWorld.width);
            var height = data.As("height", CWorld.height);

            if (width  <= 0) width  = CWorld.width;
            if (height <= 0) height = CWorld.height;

            var view = new CPanelView(width, height);
            view.transform.localposition = new Vector2(x, y);

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
