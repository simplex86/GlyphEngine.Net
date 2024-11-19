using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUITextDeserializer : IUIComponentDeserializer
    {
        public void Deserialize(JsonData data, CUIPanelView view)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var name = (string)data["name"];
            var text = (string)data["text"];

            var component = new CUIText(text, new Vector2(x, y));
            view.AddComponent(component, name);
        }
    }
}
