using LitJson;

namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal class CUIImageDeserializer : IDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        public void Deserialize(JsonData data, IContainer container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var color = ColorTransverter.Get(data, "color");
            var texture = Load(data);

            var image = new CUIImage(texture, new Vector2(x, y), color);

            var view = container as CUIPanelView;
            view.AddComponent(image, name);
        }

        /// <summary>
        /// 加载纹理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private CTexture Load(JsonData data)
        {
            if (data.AsString("texture", out var filepath))
            {
                return CResourceManager.Load(filepath, true);
            }

            return null;
        }
    }
}
