using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public static class CSceneManager
    {
        private static CSceneManagerImp cSceneManagerImp = null;

        /// <summary>
        /// 初始化
        /// </summary>
        internal static void SetSceneManagerImp(CSceneManagerImp sceneManagerImp)
        {
            cSceneManagerImp = sceneManagerImp;
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        /// <returns></returns>
        public static TScene Load<TScene>() where TScene : CScene
        {
            var scene = cSceneManagerImp.Get<TScene>();
            if (scene == null)
            {
                scene = Activator.CreateInstance<TScene>();
                cSceneManagerImp.Add(scene);

                scene.Enter();
            }

            return scene;
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        public static void Unload<TScene>() where TScene : CScene
        {
            var scene = cSceneManagerImp.Get<TScene>();
            Unload(scene);
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
                cSceneManagerImp.Remove(scene);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CScene GetMainScene()
        {
            return cSceneManagerImp.GetMainScene();
        }
    }
}
