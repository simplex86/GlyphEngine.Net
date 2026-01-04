using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CSkinDeserializer : IDeserializer<CGameObject>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="container"></param>
        public void Deserialize(JsonData data, IContainable<CGameObject> container)
        {
            var json = CResources.LoadJson((string)data);
            var renderable = container as CRenderableObject;

            var children = json["children"];
            for (int i = 0; i < children.Count; i++)
            {
                var name = children[i].As("name", "skin");
                var skin = new CSkin(name);

                DeserializePixels(children[i], skin);
                renderable.AddSkin(skin);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="skin"></param>
        private void DeserializePixels(JsonData data, CSkin skin)
        {
            var pixels = data["pixels"];
            for (int i = 0; i < pixels.Count; i++)
            {
                var p = pixels[i];

                var x = p.As("x", 0);
                var y = p.As("y", 0);
                var c = p.As("c", CChar.Empty);
                var color = CColorHelper.Get(p, "color");

                skin.Set(x, y, c, color);
            }
        }
    }
}
