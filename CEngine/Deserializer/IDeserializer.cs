using LitJson;

namespace CEngine
{
    internal interface IDeserializer<T>
    {
        public void Deserialize(JsonData data, IContainable<T> container);
    }
}
