using LitJson;

namespace SimpleX.CEngine
{
    internal interface IDeserializer
    {
        public void Deserialize(JsonData data, CGameObjectContainer container);
    }
}
