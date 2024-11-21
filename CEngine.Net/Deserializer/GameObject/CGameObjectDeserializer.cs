using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CGameObjectDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public CGameObject Deserialize(JsonData data)
        {
            return CGameObjectDeserializer.Deserialize(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parent"></param>
        public static CGameObject Deserialize(JsonData data, CGameObject parent = null)
        {
            CGameObject gameobject = null;

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
            }
            else
            {
                gameobject = new CGameObject(x, y)
                {
                    name = data.As("name", "gameobject"),
                };
            }

            if (parent != null)
            {
                parent.AddChild(gameobject);
            }

            DeserializeChildren(data, gameobject);

            return gameobject;
        }

        private static void DeserializePixels(JsonData data, CRenderableObject gameObject)
        {
            if (data.ContainsKey("pixels"))
            {
                var pixels = data["pixels"];
                for (int i = 0; i<pixels.Count; i++) 
                {
                    var p = pixels[i];

                    var x = p.As("x", 0);
                    var y = p.As("y", 0);
                    var c = p.As("c", CChar.Empty);
                    var color = ColorTransverter.Get(p, "color");

                    var pixel = CPixelPool.Instance.Alloc(x, y, c, color);
                    gameObject.AddPixel(pixel);
                }
            }
        }

        private static void DeserializeChildren(JsonData data, CGameObject gameobject)
        {
            if (data.ContainsKey("children"))
            {
                var children = data["children"];
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    Deserialize(child, gameobject);
                }
            }
        }
    }
}
