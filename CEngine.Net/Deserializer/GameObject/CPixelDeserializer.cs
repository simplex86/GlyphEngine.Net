using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPixelDeserializer : IDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, CGameObjectContainer container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var c = data.As("c", CChar.Empty);
            var color = ColorTransverter.Get(data, "color");

            var pixel = CPixelPool.Instance.Alloc(x, y, c, color);

            var renderable = container as CRenderableObject;
            renderable.AddPixel(pixel);
        }
    }
}
