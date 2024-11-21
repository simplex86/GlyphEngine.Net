using LitJson;

namespace SimpleX.CEngine.UI
{
    internal interface IUIComponentDeserializer
    {
        void Deserialize(JsonData data, CUIPanelView view);
    }
}
