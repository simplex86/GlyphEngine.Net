namespace CEngine
{
    /// <summary>
    /// 游戏对象
    /// </summary>
    public partial class CGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CGameObject Create(string name)
        {
            return Create(name, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static CGameObject Create(string name, CGameObject parent)
        {
            var gameobject = new CGameObject()
            {
                Name = name,
            };
           
            if (parent == null)
            {
                CWorld.Add(gameobject);
            }
            else
            {
                gameobject.SetParent(parent);
            }

            return gameobject;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="gameobject"></param>
        public static void Destroy(CGameObject gameobject)
        {
            if (gameobject == null ||
                gameobject.Destroyed)
            {
                return;
            }

            gameobject.Destroy();
        }
    }
}
