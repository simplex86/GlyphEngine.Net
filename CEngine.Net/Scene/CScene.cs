using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 场景
    /// </summary>
    public class CScene
    {
        private CCamera camera = null;
        private List<CObject> objects = new List<CObject>();
        private bool modified = false;

        /// <summary>
        /// 脏标记
        /// 为Ture时，表明场景需要渲染
        /// </summary>
        internal bool dirty
        {
            get
            {
                if (camera == null ||
                    camera.dirty)
                {
                    return true;
                }

                if (modified)
                {
                    return true;
                }

                foreach (var obj in objects)
                {
                    if (obj.dirty)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public CScene()
        {
            
        }

        /// <summary>
        /// 创建新对象
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <returns></returns>
        public TObject Create<TObject>() where TObject : CObject
        {
            return Create<TObject>("CGameObject");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public TObject Create<TObject>(string name) where TObject : CObject
        {
            var o = Activator.CreateInstance<TObject>();
            o.Name = name;
            
            objects.Add(o);
            modified = true;

            return o;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="o"></param>
        public void Destroy(CObject o)
        {
            objects.Remove(o);
            modified = true;
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cobject"></param>
        /// <returns></returns>
        public bool Find(string name, out CObject cobject)
        {
            cobject = null;

            foreach (var o in objects)
            {
                if (o.Name == name)
                {
                    cobject = o;
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
        /// <param name="tobject"></param>
        /// <returns></returns>
        public bool Find<TObject>(string name, out TObject tobject) where TObject : CObject
        {
            tobject = null;

            foreach (var o in objects)
            {
                if (o is TObject &&
                    o.Name == name)
                {
                    tobject = o as TObject;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 设置场景主相机
        /// 注：如果没有显示设置主相机，则使用默认主相机
        /// </summary>
        /// <param name="camera"></param>
        public void SetMainCamera(CCamera camera)
        {
            this.camera = camera;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void PrevRender()
        {
            if (camera == null && 
                CCameraManager.Get("Main", out var mainCamera))
            {
                SetMainCamera(mainCamera);
            }
        }

        /// <summary>
        /// 渲染
        /// </summary>
        internal void Render()
        {
            if (objects.Count == 0)
            {
                return;
            }

            if (dirty)
            {
                camera?.Render(objects);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void PostRender()
        {
            modified = false;
        }
    }
}
