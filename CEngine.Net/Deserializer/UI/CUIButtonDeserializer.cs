using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUIButtonDeserializer : IDeserializer
    {
        public void Deserialize(JsonData data, IContainer contaner)
        {
            var x = data.As("x", 0);
            var y = data.As("y", 0);
            var name = data.As("name", string.Empty);
            var text = data.As("text", string.Empty);
            var keycode = KeycodeTransverter.Get(data, "keycode");
            var unfocusColor = ColorTransverter.Get(data, "unfocusColor");
            var focusColor = ColorTransverter.Get(data, "focusColor");
            var border = data.As("border", EBorderStyle.ThinBorder);
            var interactable = data.As("interactable", false);
            var focus = data.As("focus", false);

            if (!interactable)
            {
                focusColor = ConsoleColor.Gray;
                unfocusColor = ConsoleColor.Gray;
                focus = false;
            }

            var button = new CUIButton(text, new Vector2(x, y), keycode, unfocusColor, focusColor, border);
            button.interactable = interactable;

            var view = contaner as CUIPanelView;
            view.AddComponent(button, name, focus);
        }
    }
}
