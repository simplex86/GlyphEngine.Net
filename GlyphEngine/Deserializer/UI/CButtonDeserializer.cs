using System;
using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    [CWidgetDeserializer(EWidgetType.Button)]
    internal class CButtonDeserializer : IDeserializer<CWidget>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contaner"></param>
        public void Deserialize(JsonData data, IContainable<CWidget> contaner)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var width = data.As("width", 8);
            var height = data.As("height", 3);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);
            var keycode = CKeycodeHelper.Get(data, "keycode");
            var unfocusColor = CColorHelper.Get(data, "unfocusColor");
            var focusColor = CColorHelper.Get(data, "focusColor");
            var border = data.As("border", EBorderStyle.Thin);
            var interactable = data.As("interactable", false);
            var focus = data.As("focus", false);

            if (!interactable)
            {
                focusColor = ConsoleColor.Gray;
                unfocusColor = ConsoleColor.Gray;
                focus = false;
            }

            if (width  <= 0) width  = CScreen.Width;
            if (height <= 0) height = CScreen.Height;

            var button = new CButton(new CVector2(x, y), width, height, keycode, unfocusColor, focusColor, border)
            {
                Name = name,
                Interactabled = interactable,
            };
            contaner.Add(button);

            if (data.TryGetValue("widgets", out var wdata))
            {
                DeserializeWidgets(wdata, button);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="button"></param>
        private void DeserializeWidgets(JsonData data, CButton button)
        {
            for (int i = 0; i < data.Count; i++)
            {
                CWidgetDeserializer.Deserialize(data[i], button);
            }
        }
    }
}
