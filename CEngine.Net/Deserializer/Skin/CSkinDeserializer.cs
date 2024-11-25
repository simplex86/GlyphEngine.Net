using LitJson;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CSkinDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public static void Deserialize(string filepath, CRenderableObject renderable)
        {
            var data = CResourceManager.LoadJson(filepath);

            var children = data["children"];
            for (int i = 0; i < children.Count; i++)
            {
                DeserializeSkin(children[i], renderable, i);
            }
        }

        private static CSkin DeserializeSkin(JsonData data, CRenderableObject renderable, int index)
        {
            var name = data.As("name", $"skin_{index}");
            var skin = new CSkin(name);

            DeserializePixels(data, skin);
            renderable.AddSkin(skin);

            return skin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="skin"></param>
        private static void DeserializePixels(JsonData data, CSkin skin)
        {
            var pixels = data["pixels"];
            for (int i = 0; i < pixels.Count; i++)
            {
                var p = pixels[i];

                var x = p.As("x", 0);
                var y = p.As("y", 0);
                var c = p.As("c", CChar.Empty);
                var color = ColorTransverter.Get(p, "color");

                skin.Set(x, y, c, color);
            }
        }
    }
}
