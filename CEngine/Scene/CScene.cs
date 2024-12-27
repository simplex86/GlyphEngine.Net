namespace CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene : CGameObjectContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public string filepath { get; }

        /// <summary>
        /// 
        /// </summary>
        internal List<CGameObject> gameobjects { get; } = new List<CGameObject>();

        /// <summary>
        /// 
        /// </summary>
        internal CScene(string filepath)
        {
            this.filepath = filepath;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Add(CGameObject gameobject)
        {
            if (!gameobjects.Contains(gameobject))
            {
                gameobjects.Add(gameobject);
            }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="gameobject"></param>
        internal override void Remove(CGameObject gameobject)
        {
            if (gameobjects.Contains(gameobject))
            {
                gameobjects.Remove(gameobject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Destroy()
        {
            foreach (var gameobject in gameobjects)
            {
                gameobject.Destroy();
            }
            gameobjects.Clear();
        }
    }
}
