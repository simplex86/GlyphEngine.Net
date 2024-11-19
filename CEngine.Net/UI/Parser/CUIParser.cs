using LitJson;

namespace SimpleX.CEngine.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CUIParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static CUIPanelView Parse(string json)
        {
            try
            {
                var data = JsonMapper.ToObject(json);
                return ParsePanel(data);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static CUIPanelView ParsePanel(JsonData data)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var width = (int)data["width"];
            var height = (int)data["height"];

            if (width  <= 0) width  = CWorld.width;
            if (height <= 0) height = CWorld.height;

            var view = new CUIPanelView(width, height);
            view.transform.position = new Vector2(x, y);

            ParseComponents(data["components"], view);

            return view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void ParseComponents(JsonData data, CUIPanelView view)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var child = data[i];
                var type = (string)child["type"];

                switch (type)
                {
                    case "text":
                        BuildText(child, view);
                        break;
                    case "button":
                        BuildButton(child, view);
                        break;
                    case "image":
                        BuildImage(child, view);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void BuildText(JsonData data, CUIPanelView view)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var name = (string)data["name"];
            var text = (string)data["text"];

            var component = new CUIText(text, new Vector2(x, y));
            view.AddComponent(component, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void BuildButton(JsonData data, CUIPanelView view)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var name = (string)data["name"];
            var text = (string)data["text"];
            var keycode = (ConsoleKey)(int)data["keycode"];
            var unfocusColor = (ConsoleColor)(int)data["unfocusColor"];
            var focusColor = (ConsoleColor)(int)data["focusColor"];
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="view"></param>
        private static void BuildImage(JsonData data, CUIPanelView view)
        {
            var x = (int)data["x"];
            var y = (int)data["y"];
            var name = (string)data["name"];
            var file = (string)data["texture"];

            var texture = new CTexture();
            texture.Load(file);

            var image = new CUIImage(texture, new Vector2(x, y));
            view.AddComponent(image, name);
        }
    }
}
