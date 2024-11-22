using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CCameraDeserializer : IDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, IContainer container)
        {
            var name = data.As("name", string.Empty);
            var order = data.As("order", 0u);

            var camera = new CCamera(name, order)
            {
                width = data.As("width", CWorld.width),
                height = data.As("height", CWorld.height),
                mask = data.As("mask", (ulong)ERenderMask.Default),
            };

            var x = data.As("x", 0);
            var y = data.As("y", 0);
            camera.transform.position = new Vector2(x, y);

            var scene = container as CScene;
            scene.Add(camera);
        }
    }
}
