using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    internal sealed class CSceneManagerImp
    {
        /// <summary>
        /// 场景列表
        /// </summary>
        internal List<CScene> scenes { get; } = new List<CScene>();
        /// <summary>
        /// 相机列表
        /// </summary>
        internal List<CCamera> cameras { get; } = new List<CCamera>();
        /// <summary>
        /// 渲染器
        /// </summary>
        private static CRenderer renderer { get; } = new CRenderer();

        /// <summary>
        /// 添加场景
        /// </summary>
        /// <param name="scene"></param>
        internal void Add(CScene scene)
        {
            scenes.Add(scene);
            foreach (var camera in scene.cameras)
            {
                cameras.Add(camera);
            }
        }

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        /// <returns></returns>
        internal TScene Get<TScene>() where TScene : CScene
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
        internal void Remove(CScene scene)
        {
            if (scenes.Count > 1)
            {
                RemoveImp(scene);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal CScene GetMainScene()
        {
            return (scenes.Count == 0) ? null : scenes[0];
        }

        /// <summary>
        /// 刷新
        /// </summary>
        internal void Update()
        {
            foreach (var scene in scenes)
            {
                if (RemoveDestroyedGameObjectsFromScene(scene))
                {
                    break;
                }
            }

            foreach (var camera in cameras)
            {
                RenderScenesByCamera(camera);
            }

            renderer.Render();
        }

        /// <summary>
        /// 移除场景，包括场景中的相机和物体
        /// </summary>
        /// <param name="scene"></param>
        private void RemoveImp(CScene scene)
        {
            scenes.Remove(scene);
            foreach (var camera in scene.cameras)
            {
                cameras.Remove(camera);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private bool RemoveDestroyedGameObjectsFromScene(CScene scene)
        {
            var gameObjects = scene.gameObjects;
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var go = gameObjects[i];
                if (go.destroyed)
                {
                    gameObjects.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="camera"></param>
        private void RenderScenesByCamera(CCamera camera)
        {
            foreach (var scene in scenes)
            {
                camera.Render(scene.gameObjects, renderer);
            }
        }
    }
}
