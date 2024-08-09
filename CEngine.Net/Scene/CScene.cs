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

        public CScene()
        {
            
        }

        internal void Enter()
        {
            OnEnter();
        }

        protected virtual void OnEnter()
        {

        }

        internal void Exit()
        {
            OnExit();
        }

        protected virtual void OnExit()
        {

        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(CGameObject gameObject)
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
        public void Remove(CGameObject gameObject)
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
        public bool Find(string name)
        {
            return Find(name, out var _);
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool Find(string name, out CGameObject gameObject)
        {
            gameObject = null;

            foreach (var o in gameObjects)
            {
                if (o.Name == name)
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
        public bool Find<TObject>(string name, out TObject gameObject) where TObject : CGameObject
        {
            gameObject = null;

            foreach (var o in gameObjects)
            {
                if (o is TObject &&
                    o.Name == name)
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
    }
}
