using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal interface ISceneComponentDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        void Deserialize(JsonData data, CScene scene);
    }
}
