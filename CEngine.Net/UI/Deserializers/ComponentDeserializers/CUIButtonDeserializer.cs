using LitJson;

namespace SimpleX.CEngine.UI
{
    internal class CUIButtonDeserializer : IUIComponentDeserializer
    {
        public void Deserialize(JsonData data, CUIPanelView view)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var name = (string)data["name"];
            var text = (string)data["text"];
            var keycode = KeycodeTransverter.Get(data, "keycode");
            var unfocusColor = ColorTransverter.Get(data, "unfocusColor");
            var focusColor = ColorTransverter.Get(data, "focusColor");
            var border = (CUIButton.Style)(int)data["border"];
            var interactable = (bool)data["interactable"];
            var focus = (bool)data["focus"];

            if (!interactable)
            {
                focusColor = ConsoleColor.Gray;
                unfocusColor = ConsoleColor.Gray;
                focus = false;
            }

            var button = new CUIButton(text, new Vector2(x, y), keycode, unfocusColor, focusColor, border);
            button.interactable = interactable;

            view.AddComponent(button, name, focus);
        }
    }
}
