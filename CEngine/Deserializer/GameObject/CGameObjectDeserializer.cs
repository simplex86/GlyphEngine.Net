using System.Collections.Generic;
using LitJson;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CGameObjectDeserializer : IDeserializer
    {
        private static Dictionary<string, IDeserializer> deserializers = new()
        {
            { "gameobject", new CGameObjectDeserializer() },
            { "skin", new CSkinDeserializer() },
            { "pixel", new CPixelDeserializer() },
            { "prefab", new CPrefabDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, IContainable<CGameObject> container)
        {
            DeserializeImp(data, container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        internal static CGameObject Deserialize(string filepath)
        {
            var data = CResources.LoadJson(filepath);
            return DeserializeImp(data, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        internal static CGameObject Deserialize(string filepath, IContainable<CGameObject> container)
        {
            var data = CResources.LoadJson(filepath);
            return DeserializeImp(data, container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private static CGameObject DeserializeImp(JsonData data, IContainable<CGameObject> container)
        {
            CGameObject gameobject;

            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var renderable = data.As("renderable", false);

            if (renderable)
            {
                var layer = data.As("layer", ERenderLayer.Default);
                gameobject = new CRenderableObject(x, y, layer, false)
                {
                    Name = data.As("name", "gameobject"),
                };

                DeserializePixels(data, gameobject as CRenderableObject);
                DeserializeSkins(data, gameobject as CRenderableObject);
            }
            else
            {
                gameobject = new CGameObject(x, y, false)
                {
                    Name = data.As("name", "gameobject"),
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
            if (data.TryGetValue("pixels", out var pixels))
            {
                var deserializer = deserializers["pixel"];
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
            if (data.TryGetValue("children", out var children))
            {
                for (int i = 0; i < children.Count; i++)
                {
                    var cdata = children[i];

                    if (cdata.AsString("type", out var type) &&
                        deserializers.TryGetValue(type, out var deserializer))
                    {
                        deserializer.Deserialize(cdata, gameobject);
                    }
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
            if (data.TryGetValue("skins", out var skins))
            {
                var deserializer = deserializers["skin"];
                for (int i = 0; i < skins.Count; i++)
                {
                    deserializer.Deserialize(skins[i], renderable);
                }
            }
        }
    }
}
