using System.Collections.Generic;

namespace CEngine
{
    /// <summary>
    /// 世界，即场景管理器
    /// </summary>
    internal static class CWorld
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
            
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="scene"></param>
        internal static void Add(CScene scene)
        {
            foreach (var camera in scene.Cameras)
            {
                Add(camera);
            }
            SortCameras();

            foreach (var gameobject in scene.GameObjects)
            {
                Add(gameobject);
            }
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
            if (!gameobjects.Contains(gameobject))
            {
                gameobjects.Add(gameobject);
                for (int i = 0; i < gameobject.Count; i++)
                {
                    Add(gameobject[i]);
                }
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
                if (gameobjects[i].Destroyed)
                {
                    gameobjects.RemoveAt(i);
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
            //SortCameras();
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
        /// 渲染
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
                if (a.Order < b.Order) return -1;
                return 1;
            });
        }
    }
}
