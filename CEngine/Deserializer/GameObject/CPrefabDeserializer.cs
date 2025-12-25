using LitJson;

namespace CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class CPrefabDeserializer : IDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scene"></param>
        public void Deserialize(JsonData data, CGameObjectContainer container)
        {
            var filepath = data.As("filepath", string.Empty);
            if (!string.IsNullOrEmpty(filepath))
            {
                var gameobject = CGameObjectDeserializer.Deserialize(filepath, container);
                if (gameobject != null)
                {
                    var x = data.As("x", 0);
                    var y = data.As("y", 0);
                    gameobject.Transform.LocalPosition = new Vector2(x, y);
                }
            }
        }
    }
}
