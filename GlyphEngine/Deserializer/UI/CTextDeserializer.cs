using LitJson;

namespace GlyphEngine
{
    internal class CTextDeserializer : IDeserializer<CWidget>
    {
        public void Deserialize(JsonData data, IContainable<CWidget> container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);
            var color = CColorHelper.Get(data, "color");

            var widget = new CText(text, new Vector2(x, y))
            {
                Name = name,
                Color = color,
            };

            container.Add(widget);
        }
    }
}
