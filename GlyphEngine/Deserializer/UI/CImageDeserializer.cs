using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CImageDeserializer : IDeserializer<CWidget>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        public void Deserialize(JsonData data, IContainable<CWidget> container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var color = CColorHelper.Get(data, "color");
            var transparent = data.As("transparent", false);
            var texture = Load(data, transparent);

            var image = new CImage(texture, new CVector2(x, y), color)
            {
                Name = name,
            };

            container.Add(image);
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
