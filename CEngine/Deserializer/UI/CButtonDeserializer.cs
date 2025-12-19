using System;
using LitJson;

namespace CEngine.UI
{
    internal class CButtonDeserializer : IDeserializer
    {
        public void Deserialize(JsonData data, CGameObjectContainer contaner)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);
            var keycode = CKeycodeHelper.Get(data, "keycode");
            var unfocusColor = CColorHelper.Get(data, "unfocusColor");
            var focusColor = CColorHelper.Get(data, "focusColor");
            var border = data.As("border", EBorderStyle.ThinBorder);
            var interactable = data.As("interactable", false);
            var focus = data.As("focus", false);

            if (!interactable)
            {
                focusColor = ConsoleColor.Gray;
                unfocusColor = ConsoleColor.Gray;
                focus = false;
            }

            var button = new CButton(text, new Vector2(x, y), keycode, unfocusColor, focusColor, border);
            button.interactable = interactable;

            var view = contaner as CPanelView;
            view.AddComponent(button, name, focus);
        }
    }
}
