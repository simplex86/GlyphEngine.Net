using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUITextDeserializer : IUIComponentDeserializer
    {
        public void Deserialize(JsonData data, CUIPanelView view)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);

            var component = new CUIText(text, new Vector2(x, y));
            view.AddComponent(component, name);
        }
    }
}
