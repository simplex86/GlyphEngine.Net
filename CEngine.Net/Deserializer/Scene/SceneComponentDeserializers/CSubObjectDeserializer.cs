using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CSubObjectDeserializer : ISceneComponentDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, CScene scene)
        {
            var gameobject = CGameObjectDeserializer.Deserialize(data);
            scene.Add(gameobject);
        }
    }
}
