using LitJson;

namespace CEngine
{
    internal interface IDeserializer
    {
        public void Deserialize(JsonData data, CGameObjectContainer container);
    }
}
