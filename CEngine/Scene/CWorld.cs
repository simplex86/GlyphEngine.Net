using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 世界，即场景管理器
    /// </summary>
    internal static class CWorld
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
        /// 初始化
        /// </summary>
        internal static void Init()
        {

        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="scene"></param>
        internal static void Add(CScene scene)
        {
            scenes.Add(scene);

            foreach (var camera in scene.Cameras)
            {
                Add(camera);
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
        /// <param name="camera"></param>
        internal static void Add(CCamera camera)
        {
            if (!cameras.Contains(camera))
            {
                cameras.Add(camera);
            }
            SortCameras();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="camera"></param>
        internal static void Remove(CCamera camera)
        {
            cameras.Remove(camera);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameobject"></param>
        internal static void Add(CGameObject gameobject)
        {
            var current = GetCurrentScene();
            current?.Add(gameobject);
        }

        /// <summary>
        /// 更新场景
        /// </summary>
        internal static void Update(float dt)
        {
            PrevProcess();
            {
                Render();
            }
            PostProcess();
        }

        /// <summary>
        /// 更新场景预处理
        /// </summary>
        private static void PrevProcess()
        {
            // 从场景列表中移除已被销毁的场景
            for (int i = scenes.Count - 1; i >= 0; i--)
            {
                if (scenes[i].Destroyed)
                {
                    scenes.RemoveAt(i);
                }
            }

            // 从相机列表中移除已被销毁的相机
            for (int i = cameras.Count - 1; i >= 0; i--)
            {
                if (cameras[i].Destroyed)
                {
                    cameras.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 渲染
        /// </summary>
        private static void Render()
        {
            foreach (var camera in cameras)
            {
                if (!camera.Enabled) continue;

                foreach (var scene in scenes)
                {
                    camera.Render(scene.GameObjects, renderer);
                }
            }
            renderer.Render();
        }

        /// <summary>
        /// 更新场景后处理
        /// </summary>
        private static void PostProcess()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static CScene GetCurrentScene()
        {
            for (int i=scenes.Count - 1; i >= 0; i--)
            {
                if (!scenes[i].Destroyed) return scenes[i];
            }

            return null;
        }

        /// <summary>
        /// 相机排序
        /// </summary>
        private static void SortCameras()
        {
            cameras.Sort((a, b) =>
            {
                if (a.Order < b.Order) return -1;
                return 1;
            });
        }
    }
}
