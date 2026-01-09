using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPrefabDeserializer : IDeserializer<CGameObject>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, IContainable<CGameObject> container)
        {
            var filepath = data.As("filepath", string.Empty);
            if (!string.IsNullOrEmpty(filepath))
            {
                var gameobject = CGameObjectDeserializer.Deserialize(filepath, container);
                if (gameobject != null)
                {
                    var x = data.As("x", 0);
                    var y = data.As("y", 0);
                    gameobject.Transform.LocalPosition = new CVector2(x, y);
                    gameobject.Name = data.As("name", gameobject.Name);
                }
            }
        }
    }
}
