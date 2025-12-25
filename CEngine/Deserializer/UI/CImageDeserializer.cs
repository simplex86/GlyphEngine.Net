using LitJson;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CImageDeserializer : IDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        public void Deserialize(JsonData data, CGameObjectContainer container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var color = CColorHelper.Get(data, "color");
            var transparent = data.As("transparent", false);
            var texture = Load(data, transparent);

            var image = new CImage(texture, new Vector2(x, y), color);

            var view = container as CPanelView;
            view.AddComponent(image, name);
        }

        /// <summary>
        /// 加载纹理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private CTexture Load(JsonData data, bool transparent)
        {
            if (data.AsString("texture", out var filepath))
            {
                return CResources.LoadTex(filepath, transparent);
            }

            return null;
        }
    }
}
