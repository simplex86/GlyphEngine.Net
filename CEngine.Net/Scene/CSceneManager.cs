namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public static class CSceneManager
    {
        /// <summary>
        /// 
        /// </summary>
        internal static List<CGameObject> gameobjects = new List<CGameObject>(1000);
        /// <summary>
        /// 
        /// </summary>
        internal static List<CRenderableObject> renderobjects = new List<CRenderableObject>(1000);

        /// <summary>
        /// 场景列表
        /// </summary>
        private static List<CScene> scenes = new List<CScene>();
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
        public static void Init()
        {
            Add(new CDontDestroyScene());
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        /// <returns></returns>
        internal static void Add(CScene scene)
        {
            scenes.Add(scene);

            foreach (var gameobject in scene.gameobjects)
            {
                Add(gameobject);
            }
            foreach (var camera in scene.cameras)
            {
                cameras.Add(camera);
            }
            SortCameras();

            scene.Init();
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
        /// 查找指定的相机
        /// </summary>
        /// <param name="name"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool FindCamera(string name, out CCamera camera)
        {
            camera = null;

            foreach (var cam in cameras)
            {
                if (cam.name == name)
                {
                    camera = cam;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        internal static void Add(CGameObject gameobject)
        {
            gameobjects.Add(gameobject);
            if (gameobject is CRenderableObject renderobject)
            {
                renderobjects.Add(renderobject);
            }

            foreach (var child in gameobject.children)
            {
                Add(child);
            }
        }

        /// <summary>
        /// 更新场景预处理
        /// </summary>
        private static void PrevUpdate(float dt)
        {
            RemoveDestroyedObjects();
            SortCameras();
        }

        /// <summary>
        /// 更新场景
        /// </summary>
        internal static void Update(float dt)
        {
            PrevUpdate(dt);
            {
                foreach (var scene in scenes)
                {
                    scene.behaviour?.Update(dt);
                }
                foreach (var camera in cameras)
                {
                    RenderByCamera(camera);
                }
                renderer.Render();
            }
            PostUpdate(dt);
        }

        /// <summary>
        /// 更新场景后处理
        /// </summary>
        private static void PostUpdate(float dt)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private static void RemoveDestroyedObjects()
        {
            // 从场景列表中移除已被销毁的场景
            for (int i = scenes.Count - 1; i >= 0; i--)
            {
                if (scenes[i].destroyed)
                {
                    scenes.RemoveAt(i);
                }
            }
            // 从对象列表中移除已被销毁的对象
            for (int i = gameobjects.Count - 1; i >= 0; i--)
            {
                if (gameobjects[i].destroyed)
                {
                    gameobjects.RemoveAt(i);
                }
            }
            // 从可渲染对象列表中移除已被销毁的对象
            for (int i = renderobjects.Count - 1; i >= 0; i--)
            {
                if (renderobjects[i].destroyed)
                {
                    renderobjects.RemoveAt(i);
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
        }

        /// <summary>
        /// 渲染场景
        /// </summary>
        /// <param name="camera"></param>
        private static void RenderByCamera(CCamera camera)
        {
            camera.Render(renderobjects, renderer);
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
