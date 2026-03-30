using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CProgressBarDeserializer : IDeserializer<CWidget>
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
            var style = data.As("style", EProgressBarStyle.Horizontal);
            var length = data.As("length", 10);
            var amount = data.As("amount", 0.0f);
            var color = data.As("color", CColor.White);
            var direction = data.As("direction", EProgressBarDirection.Left);

            var widget = new CProgressBar(length, new CVector2(x, y), amount, style, direction)
            {
                Name = name,
                Amount = amount,
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
        private void DeserializeWidgets(JsonData data, CProgressBar widget)
        {
            for (int i = 0; i < data.Count; i++)
            {
                CWidgetDeserializer.Deserialize(data[i], widget);
            }
        }
    }
}
