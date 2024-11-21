using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CSceneDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, ISceneComponentDeserializer> deserializers = new()
        {
            { "camera", new CCameraDeserializer() },
            { "gameobject", new CSubObjectDeserializer() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static void Deserialize(string file, CScene scene)
        {
            var data = ResourceManager.LoadJson(file);
            Deserialize(data, scene);
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
                if (data[i].AsString("type", out var type))
                {
                    if (deserializers.TryGetValue(type, out var deserializer))
                    {
                        deserializer.Deserialize(data[i], scene);
                    }
                }
            }
        }
    }
}
