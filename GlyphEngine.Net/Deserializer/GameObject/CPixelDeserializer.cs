using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPixelDeserializer : IDeserializer<CGameObject>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, IContainable<CGameObject> container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var glyph = data.As("glyph", CGlyph.Empty);
            var color = data.As("color", CScreen.BackgroundColor);

            var pixel = new CPixel(x, y, glyph, color);

            var renderable = container as CRenderableObject;
            renderable.AddPixel(pixel);
        }
    }
}
