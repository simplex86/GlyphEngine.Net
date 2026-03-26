using System.Collections.Generic;
using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CSceneDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, IDeserializer<CGameObject>> deserializers = new()
        {
            { "camera", new CCameraDeserializer() },
            { "gameobject", new CGameObjectDeserializer() },
            { "prefab", new CPrefabDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CScene Deserialize(string filepath)
        {
            var scene = new CScene();
            {
                var data = CResources.LoadJson(filepath);
                Deserialize(data, scene);
            }
            return scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static void Deserialize(JsonData data, CScene scene)
        {
            if (data.AsString("name", out var name))
            {
                scene.Name = name;
            }

            if (data.TryGetValue("children", out var children))
            {
                for (int i = 0; i < children.Count; i++)
                {
                    if (children[i].AsString("type", out var type) &&
                        deserializers.TryGetValue(type, out var deserializer))
                    {
                        deserializer.Deserialize(children[i], scene);
                    }
                }
            }
        }
    }
}
