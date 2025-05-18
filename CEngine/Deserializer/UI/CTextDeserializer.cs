using LitJson;

namespace CEngine.UI
{
    internal class CTextDeserializer : IDeserializer
    {
        public void Deserialize(JsonData data, CGameObjectContainer container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);
            var color = CColorHelper.Get(data, "color");

            var component = new CText(text, new Vector2(x, y))
            {
                color = color,
            };

            var view = container as CPanelView;
            view.AddComponent(component, name);
        }
    }
}
