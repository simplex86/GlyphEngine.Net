using System.Collections.Generic;
using LitJson;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CSceneDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, IDeserializer> deserializers = new()
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
            var scene = new CScene(filepath);
            {
                var data = CResourceManager.LoadJson(filepath);
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
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].AsString("type", out var type) &&
                    deserializers.TryGetValue(type, out var deserializer))
                {
                    deserializer.Deserialize(data[i], scene);
                }
            }
        }
    }
}
