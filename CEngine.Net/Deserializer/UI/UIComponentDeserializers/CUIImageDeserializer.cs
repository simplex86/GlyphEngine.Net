using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUIImageDeserializer : IUIComponentDeserializer
    {
        public void Deserialize(JsonData data, CUIPanelView view)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
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
            if (data.AsString("texture", out var filepath))
            {
                return new CTexture(filepath);
            }

            return null;
        }
    }
}
