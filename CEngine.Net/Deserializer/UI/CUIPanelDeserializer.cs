using LitJson;

namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CUIPanelDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, IUIComponentDeserializer> deserializers = new()
        {
            { "text", new CUITextDeserializer() },
            { "button", new CUIButtonDeserializer() },
            { "image", new CUIImageDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static CUIPanelView Deserialize(string file)
        {
            var data = ResourceManager.LoadJson(file);
            return Deserialize(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static CUIPanelView Deserialize(JsonData data)
        {
            var x = data.ContainsKey("x") ? (int)data["x"] : 0;
            var y = data.ContainsKey("y") ? (int)data["y"] : 0;
            var width  = data.ContainsKey("width")  ? (int)data["width"]  : CWorld.width;
            var height = data.ContainsKey("height") ? (int)data["height"] : CWorld.height;

            if (width  <= 0) width  = CWorld.width;
            if (height <= 0) height = CWorld.height;

            var view = new CUIPanelView(width, height);
            view.transform.position = new Vector2(x, y);

            DeserializeComponents(data["components"], view);

            return view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void DeserializeComponents(JsonData data, CUIPanelView view)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].ContainsKey("type"))
                {
                    var type = (string)data[i]["type"];
                    if (deserializers.TryGetValue(type, out var deserializer))
                    {
                        deserializer.Deserialize(data[i], view);
                    }
                }
            }
        }
    }
}
