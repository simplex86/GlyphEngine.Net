using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CGameObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "GameObject";

        /// <summary>
        /// 可见性
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public CTransform Transform { get; }

        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get { return Transform.X; }
            set { Transform.X = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get { return Transform.Y; }
            set { Transform.Y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CGameObject Parent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Count => children.Count;

        /// <summary>
        /// 
        /// </summary>
        internal List<CGameObject> children { get; } = new List<CGameObject>();

        /// <summary>
        /// 
        /// </summary>
        internal List<CPixel> pixels { get; } = new List<CPixel>();

        /// <summary>
        /// 
        /// </summary>
        internal bool destroyed { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, CSkin> skins = new Dictionary<string, CSkin>();

        /// <summary>
        /// 
        /// </summary>
        protected CGameObject()
            : this(0, 0)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected CGameObject(string name)
            : this(0, 0)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected CGameObject(int x, int y)
        {
            Transform = new CTransform()
            {
                X = x,
                Y = y,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected CGameObject(string name, int x, int y)
            : this(x, y)
        {
            Name = name;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(CGameObject child)
        {
            child.SetParent(this);
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject GetChild(int index)
        {
            if (index < 0)
            {
                index = children.Count + index;
            }
            return children[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CGameObject this[int index]
        {
            get { return GetChild(index); }
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(CGameObject child)
        {
            children.Remove(child);
            child.SetParent(null);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(CGameObject parent)
        {
            if (this.destroyed) return;
            if (parent.destroyed) return;
            if (Parent == parent) return;

            Parent?.RemoveChild(this);
            Parent = parent;
            Parent?.children.Add(this);

            if (Parent == null)
            {
                var scene = CSceneManager.GetMainScene();
                scene.Add(this);
            }
            else
            {
                var scene = CSceneManager.GetMainScene();
                scene.Remove(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skin"></param>
        protected void AddSkin(CSkin skin)
        {
            if (skins.TryGetValue(skin.Name, out var _))
            {
                skins[skin.Name] = skin;
            }
            else
            {
                skins.Add(skin.Name, skin);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinName"></param>
        public void ApplySkin(string skinName)
        {
            if (skins.TryGetValue(skinName, out var skin))
            {
                ApplySkin(skin);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skin"></param>
        protected void ApplySkin(CSkin skin)
        {
            foreach (var pixel in pixels)
            {
                if (skin.Get(pixel.X, pixel.Y, out var spixel))
                {
                    pixel.Symbol = spixel.Symbol;
                    pixel.Color = spixel.Color;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinName"></param>
        protected void RemoveSkin(string skinName)
        {
            skins.Remove(skinName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixel"></param>
        protected void AddPixel(CPixel pixel)
        {
            pixels.Add(pixel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CGameObject CreatePrimitive()
        {
            // TODO
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <returns></returns>
        public static TGameObject Load<TGameObject>() where TGameObject : CGameObject
        {
            var gameObject = Activator.CreateInstance<TGameObject>();
            if (gameObject != null)
            {
                var scene = CSceneManager.GetMainScene();
                scene.Add(gameObject);
            }

            return gameObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TGameObject Load<TGameObject>(string name) where TGameObject : CGameObject
        {
            var gameObject = Activator.CreateInstance<TGameObject>();
            if (gameObject != null)
            {
                gameObject.Name = name;

                var scene = CSceneManager.GetMainScene();
                scene.Add(gameObject);
            }
            return gameObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TGameObject Load<TGameObject>(int x, int y) where TGameObject : CGameObject
        {
            var gameObject = Activator.CreateInstance<TGameObject>();
            if (gameObject != null)
            {
                gameObject.Transform.SetXY(x, y);

                var scene = CSceneManager.GetMainScene();
                scene.Add(gameObject);
            }

            return gameObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(CGameObject gameObject)
        {
            if (gameObject == null ||
                gameObject.destroyed)
            {
                return;
            }

            gameObject.destroyed = true;

            var parent = gameObject.Parent;
            if (parent != null)
            {
                parent.RemoveChild(gameObject);
                gameObject.SetParent(null);
            }
        }
    }
}
