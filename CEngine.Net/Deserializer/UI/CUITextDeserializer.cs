using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUITextDeserializer : IDeserializer
    {
        public void Deserialize(JsonData data, IContainer container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);

            var component = new CUIText(text, new Vector2(x, y));

            var view = container as CUIPanelView;
            view.AddComponent(component, name);
        }
    }
}
