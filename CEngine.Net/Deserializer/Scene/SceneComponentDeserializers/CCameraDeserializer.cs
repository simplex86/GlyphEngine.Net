using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CCameraDeserializer : ISceneComponentDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, CScene scene)
        {
            var name = data.As("name", string.Empty);
            var order = data.As("order", 0u);
            var x = data.As("x", 0);
            var y = data.As("y", 0);

            var camera = new CCamera(name, order)
            {
                position = new Vector2(x, y),
                width = data.As("width", CWorld.width),
                height = data.As("height", CWorld.height),
                mask = data.As("mask", (ulong)ERenderMask.Default),
            };
            scene.Add(camera);
        }
    }
}
