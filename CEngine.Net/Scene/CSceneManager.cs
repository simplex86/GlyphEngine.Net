namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public static class CSceneManager
    {
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
        /// 
        /// </summary>
        private static List<CScene> addScenes = new List<CScene>();
        /// <summary>
        /// 
        /// </summary>
        private static List<CCamera> addCameras = new List<CCamera>();
        /// <summary>
        /// 
        /// </summary>
        private static List<CScene> removeScenes = new List<CScene>();
        /// <summary>
        /// 
        /// </summary>
        private static List<CCamera> removeCameras = new List<CCamera>();

        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            Load<CDontDestroyScene>(true);
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        /// <returns></returns>
        public static TScene Load<TScene>() where TScene : CScene, new()
        {
            return Load<TScene>(false);
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="scene"></param>
        public static void Unload(CScene scene)
        {
            if (scene != null)
            {
                scene.Exit();
                Remove(scene);
            }
        }

        /// <summary>
        /// 获取主场景
        /// </summary>
        /// <returns></returns>
        public static CScene GetMainScene()
        {
            return (scenes.Count == 0) ? null : scenes[0];
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
        /// <typeparam name="TScene"></typeparam>
        /// <param name="immediately"></param>
        /// <returns></returns>
        public static TScene Load<TScene>(bool immediately) where TScene : CScene, new()
        {
            var scene = Get<TScene>();
            if (scene == null)
            {
                scene = new TScene();
                Add(scene, immediately);

                scene.Enter();
            }

            return scene;
        }

        /// <summary>
        /// 添加场景
        /// </summary>
        /// <param name="scene"></param>
        private static void Add(CScene scene, bool immediately = false)
        {
            if (immediately)
            {
                scenes.Add(scene);
                foreach (var camera in scene.cameras)
                {
                    cameras.Add(camera);
                }
            }
            else
            {
                addScenes.Add(scene);
                foreach (var camera in scene.cameras)
                {
                    addCameras.Add(camera);
                }
            }
        }

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        /// <returns></returns>
        private static TScene Get<TScene>() where TScene : CScene
        {
            foreach (var scene in scenes)
            {
                if (scene is TScene)
                {
                    return scene as TScene;
                }
            }
            return null;
        }

        /// <summary>
        /// 移除场景
        /// </summary>
        /// <param name="scene"></param>
        private static void Remove(CScene scene)
        {
            removeScenes.Add(scene);
            foreach (var camera in scene.cameras)
            {
                removeCameras.Add(camera);
            }
        }

        /// <summary>
        /// 更新场景
        /// </summary>
        internal static void Update(float dt)
        {
            foreach (var scene in addScenes)
            {
                scenes.Add(scene);
            }
            foreach (var camera in addCameras)
            {
                cameras.Add(camera);
            }

            foreach (var scene in scenes)
            {
                scene.RemoveDestroyedGameObjects();
                scene.Update(dt);
            }

            foreach (var scene in removeScenes)
            {
                scenes.Remove(scene);
            }
            foreach (var camera in removeCameras)
            {
                cameras.Remove(camera);
            }

            foreach (var camera in cameras)
            {
                RenderScenesByCamera(camera);
            }

            renderer.Render();
        }

        /// <summary>
        /// 渲染场景
        /// </summary>
        /// <param name="camera"></param>
        private static void RenderScenesByCamera(CCamera camera)
        {
            foreach (var scene in scenes)
            {
                camera.Render(scene.gameObjects, renderer);
            }
        }
    }
}
