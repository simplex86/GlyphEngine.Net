using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CGameObjectDeserializer : IDeserializer
    {
        private static Dictionary<string, IDeserializer> deserializers = new()
        {
            { "child", new CGameObjectDeserializer() },
            { "pixel", new CPixelDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, IContainer container)
        {
            DeserializeObject(data, container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CGameObject Deserialize(string filepath)
        {
            var data = CResourceManager.LoadJson(filepath);
            return DeserializeObject(data, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private static CGameObject DeserializeObject(JsonData data, IContainer container)
        {
            CGameObject gameobject;

            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var renderable = data.As("renderable", false);

            if (renderable)
            {
                var layer = data.As("layer", ERenderLayer.Default);
                gameobject = new CRenderableObject(x, y, layer)
                {
                    name = data.As("name", "gameobject"),
                };

                DeserializePixels(data, gameobject as CRenderableObject);
                DeserializeSkins(data, gameobject as CRenderableObject);
            }
            else
            {
                gameobject = new CGameObject(x, y)
                {
                    name = data.As("name", "gameobject"),
                };
            }
            DeserializeChildren(data, gameobject);

            if (container != null)
            {
                container.Add(gameobject);
            }

            return gameobject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="renderable"></param>
        private static void DeserializePixels(JsonData data, CRenderableObject renderable)
        {
            if (data.ContainsKey("pixels"))
            {
                var deserializer = deserializers["pixel"];

                var pixels = data["pixels"];
                for (int i = 0; i<pixels.Count; i++) 
                {
                    deserializer.Deserialize(pixels[i], renderable);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="gameobject"></param>
        private static void DeserializeChildren(JsonData data, CGameObject gameobject)
        {
            if (data.ContainsKey("children"))
            {
                var deserializer = deserializers["child"];

                var children = data["children"];
                for (int i = 0; i < children.Count; i++)
                {
                    deserializer.Deserialize(children[i], gameobject);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="renderable"></param>
        private static void DeserializeSkins(JsonData data, CRenderableObject renderable)
        {
            if (data.ContainsKey("skins"))
            {
                var skins = data["skins"];
                for (int i = 0; i < skins.Count; i++)
                {
                    var path = (string)skins[i];
                    CSkinDeserializer.Deserialize(path, renderable);
                }
            }
        }
    }
}
