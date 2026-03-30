using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CTextDeserializer : IDeserializer<CWidget>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        public void Deserialize(JsonData data, IContainable<CWidget> container)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);
            var color = data.As("color", CColor.White);

            var widget = new CText(text, new CVector2(x, y))
            {
                Name = name,
                Color = color,
            };
            container.Add(widget);

            if (data.TryGetValue("widgets", out var wdata))
            {
                DeserializeWidgets(wdata, widget);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="text"></param>
        private void DeserializeWidgets(JsonData data, CText text)
        {
            for (int i = 0; i < data.Count; i++)
            {
                CWidgetDeserializer.Deserialize(data[i], text);
            }
        }
    }
}
