using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUIImageDeserializer : IUIComponentDeserializer
    {
        public void Deserialize(JsonData data, CUIPanelView view)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var name = (string)data["name"];
            var color = ColorTransverter.Get(data, "color");
            var texture = LoadTexture(data);

            var image = new CUIImage(texture, new Vector2(x, y), color);
            view.AddComponent(image, name);
        }

        /// <summary>
        /// 加载纹理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private CTexture LoadTexture(JsonData data)
        {
            if (data.ContainsKey("texture"))
            {
                var texture = new CTexture((string)data["texture"]);
                return texture;
            }

            return null;
        }
    }
}
