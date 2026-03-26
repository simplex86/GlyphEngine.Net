using LitJson;

namespace GlyphEngine
{
    internal interface IDeserializer<T>
    {
        public void Deserialize(JsonData data, IContainable<T> container);
    }
}
