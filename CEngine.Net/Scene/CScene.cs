using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene
    {
        internal List<CCamera> cameras = new List<CCamera>();
        internal List<CGameObject> gameObjects = new List<CGameObject>();

        protected CScene()
        {
            
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameObject"></param>
        internal void Add(CGameObject gameObject)
        {
            if (!gameObjects.Contains(gameObject))
            {
                gameObjects.Add(gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        internal void Remove(CGameObject gameObject)
        {
            if (gameObjects.Contains(gameObject))
            {
                gameObjects.Remove(gameObject);
            }
        }

        /// <summary>
        /// 查找对象是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool Find(string name)
        {
            return Find(name, out var _);
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal bool Find(string name, out CGameObject gameObject)
        {
            gameObject = null;

            foreach (var o in gameObjects)
            {
                if (o.name == name)
                {
                    gameObject = o;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="name"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal bool Find<TObject>(string name, out TObject gameObject) where TObject : CGameObject
        {
            gameObject = null;

            foreach (var o in gameObjects)
            {
                if (o is TObject &&
                    o.name == name)
                {
                    gameObject = o as TObject;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 添加相机
        /// </summary>
        /// <param name="camera"></param>
        protected void Add(CCamera camera)
        {
            cameras.Add(camera);
        }

        /// <summary>
        /// 移除场景中已销毁的对象
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        internal void RemoveDestroyedGameObjects()
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                var go = gameObjects[i];
                if (go.destroyed)
                {
                    gameObjects.RemoveAt(i);
                }
            }
        }
    }
}
