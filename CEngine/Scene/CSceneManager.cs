namespace CEngine
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    internal static class CSceneManager
    {
        /// <summary>
        /// 对象列表
        /// </summary>
        private static List<CGameObject> gameobjects = new List<CGameObject>(1000);
        /// <summary>
        /// 相机列表
        /// </summary>
        private static List<CCamera> cameras = new List<CCamera>();
        /// <summary>
        /// 渲染器
        /// </summary>
        private static CRenderer renderer = new CRenderer();

        /// <summary>
        /// 初始化
        /// </summary>
        internal static void Init()
        {
            Add(new CCamera("ui_camera", uint.MaxValue)
            {
                mask = (ulong)ERenderMask.UI,
            });
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="scene"></param>
        internal static void Add(CScene scene)
        {
            foreach (var gameobject in scene.gameobjects)
            {
                Add(gameobject);
            }
            SortCameras();
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="scene"></param>
        internal static void Remove(CScene scene)
        {
            scene?.Destroy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        internal static void Add(CGameObject gameobject)
        {
            if (cameras.Contains(gameobject) || 
                gameobjects.Contains(gameobject))
            {
                return;
            }

            if (gameobject is CCamera camera)
            {
                cameras.Add(camera);
            }
            else
            {
                gameobjects.Add(gameobject);
            }

            for (int i=0; i<gameobject.count; i++) 
            {
                Add(gameobject[i]);
            }
        }

        /// <summary>
        /// 更新场景预处理
        /// </summary>
        private static void PrevUpdate()
        {
            // 从对象列表中移除已被销毁的对象
            for (int i = gameobjects.Count - 1; i >= 0; i--)
            {
                if (gameobjects[i].destroyed)
                {
                    gameobjects.RemoveAt(i);
                }
            }
            // 从相机列表中移除已被销毁的相机
            for (int i = cameras.Count - 1; i >= 0; i--)
            {
                if (cameras[i].destroyed)
                {
                    cameras.RemoveAt(i);
                }
            }
            SortCameras();
        }

        /// <summary>
        /// 更新场景
        /// </summary>
        internal static void Update(float dt)
        {
            PrevUpdate();
            {
                Render();
            }
            PostUpdate();
        }

        /// <summary>
        /// 更新场景后处理
        /// </summary>
        private static void PostUpdate()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private static void Render()
        {
            foreach (var camera in cameras)
            {
                camera.Render(gameobjects, renderer);
            }
            renderer.Render();
        }

        /// <summary>
        /// 相机排序
        /// </summary>
        private static void SortCameras()
        {
            cameras.Sort((a, b) =>
            {
                if (a.order < b.order) return -1;
                return 1;
            });
        }
    }
}
