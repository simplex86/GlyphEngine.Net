using System.Collections.Generic;

namespace GlyphEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene : IContainable<CGameObject>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// 物体数量
        /// </summary>
        public int Count => GameObjects.Count;
        /// <summary>
        /// 是否已经被销毁
        /// </summary>
        public bool Destroyed { get; private set; } = false;

        /// <summary>
        /// 场景内的相机列表
        /// </summary>
        internal List<CCamera> Cameras { get; } = new List<CCamera>();
        /// <summary>
        /// 场景内的对象列表
        /// </summary>
        internal List<CGameObject> GameObjects { get; } = new List<CGameObject>();

        /// <summary>
        /// 
        /// </summary>
        internal CScene()
        {

        }

        /// <summary>
        /// 添加相机
        /// </summary>
        /// <param name="camera"></param>
        internal void Add(CCamera camera)
        {
            Cameras.Add(camera);
        }

        /// <summary>
        /// 根据指定名字获取相机
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CCamera GetCamera(string name)
        {
            foreach (var camera in Cameras)
            {
                if (camera.Name == name) return camera;
            }
            return null;
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameobject"></param>
        public void Add(CGameObject gameobject)
        {
            if (!GameObjects.Contains(gameobject))
            {
                GameObjects.Add(gameobject);
            }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="gameobject"></param>
        public void Remove(CGameObject gameobject)
        {
            if (GameObjects.Contains(gameobject))
            {
                GameObjects.Remove(gameobject);
            }
        }

        /// <summary>
        /// 根据指定名字获取对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CGameObject GetChild(string name)
        {
            foreach (var gameobject in GameObjects)
            {
                if (gameobject.Name == name) return gameobject;
            }
            return null;
        }

        /// <summary>
        /// 根据指定索引获取对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject GetChild(int index)
        {
            if (index < 0)
            {
                index = GameObjects.Count + index;
            }
            return GameObjects[index];
        }

        /// <summary>
        /// 更新
        /// </summary>
        internal void Update()
        {
            if (Destroyed) return;
            //
            for (int i = Cameras.Count - 1; i >= 0; i--)
            {
                if (Cameras[i].Destroyed)
                {
                    Cameras.RemoveAt(i);
                }
            }
            // 
            for (int i = GameObjects.Count - 1 ; i >= 0; i--)
            {
                if (GameObjects[i].Destroyed)
                {
                    GameObjects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 销毁场景
        /// </summary>
        internal void Destroy()
        {
            // 销毁相机
            foreach (var camera in Cameras)
            {
                camera.Destroy();
            }
            Cameras.Clear();
            // 销毁物件
            foreach (var gameobject in GameObjects)
            {
                gameobject.Destroy();
            }
            GameObjects.Clear();
            // 标记
            Destroyed = true;
        }
    }
}
