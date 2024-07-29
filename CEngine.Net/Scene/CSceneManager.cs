using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public class CSceneManager
    {
        internal static List<CScene> scenes { get; } = new List<CScene>();

        /// <summary>
        /// 初始化
        /// </summary>
        internal static void Start()
        {
            // 加载默认的启动场景
            var launchScene = new CLaunchScene();
            scenes.Add(launchScene);
        }

        /// <summary>
        /// 更新
        /// </summary>
        internal static void Update()
        {
            foreach (var scene in scenes)
            {
                scene.PrevRender();
                scene.Render();
                scene.PostRender();
            }
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        /// <returns></returns>
        public static TScene Load<TScene>() where TScene : CScene
        {
            foreach (var tscene in scenes)
            {
                if (tscene is TScene)
                {
                    return tscene as TScene;
                }
            }

            var scene = Activator.CreateInstance<TScene>();
            scenes.Add(scene);

            return scene;
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        public static void Unload<TScene>()
        {
            if (scenes.Count == 1)
            {
                return;
            }

            foreach (var scene in scenes)
            {
                if (scene is TScene)
                {
                    scenes.Remove(scene);
                    break;
                }
            }
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="scene"></param>
        public static void Unload(CScene scene)
        {
            if (scenes.Count == 1)
            {
                return;
            }

            scenes.Remove(scene);
        }
    }
}
