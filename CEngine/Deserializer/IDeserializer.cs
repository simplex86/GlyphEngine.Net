using LitJson;

namespace CEngine
{
    internal interface IDeserializer
    {
        public void Deserialize(JsonData data, IContainable<CGameObject> container);
    }
}
