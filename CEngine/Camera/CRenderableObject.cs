using System;
using System.Collections.Generic;
using System.Linq;

namespace CEngine
{
    /// <summary>
    /// 可渲染对象
    /// </summary>
    public class CRenderableObject : CGameObject, IRenderable, ISkinable
    {
        /// <summary>
        /// 渲染层级
        /// </summary>
        public ulong layer { get; }

        /// <summary>
        /// 像素列表
        /// </summary>
        private List<CPixel> pixels = new List<CPixel>();
        /// <summary>
        /// 皮肤
        /// </summary>
        private Dictionary<string, CSkin> skins = new Dictionary<string, CSkin>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        internal protected CRenderableObject(ERenderLayer layer)
            : this(0, 0, layer)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        private protected CRenderableObject(ulong layer)
            : base(0, 0)
        {
            this.layer = layer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layer"></param>
        internal protected CRenderableObject(int x, int y, ERenderLayer layer)
            : this(x, y, layer, true)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layer"></param>
        /// <param name="scene"></param>
        internal protected CRenderableObject(int x, int y, ERenderLayer layer, bool scene)
            : base(x, y, scene)
        {
            this.layer = (ulong)layer;
        }

        /// <summary>
        /// 遍历像素
        /// </summary>
        /// <param name="action"></param>
        public void Foreach(Action<CPixel> action)
        {
            if (action == null) return;

            foreach (var pixel in pixels)
            {
                action.Invoke(pixel);
            }
        }

        /// <summary>
        /// 添加像素
        /// </summary>
        /// <param name="pixel"></param>
        internal void AddPixel(CPixel pixel)
        {
            pixels.Add(pixel);
        }

        /// <summary>
        /// 清空像素
        /// </summary>
        protected void ClearPixels()
        {
            CPixelPool.Instance.Release(pixels);
        }

        /// <summary>
        /// 添加皮肤
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skin"></param>
        internal void AddSkin(CSkin skin, bool apply = false)
        {
            if (skins.TryGetValue(skin.name, out var _))
            {
                skins[skin.name] = skin;
            }
            else
            {
                skins.Add(skin.name, skin);
            }

            if (apply)
            {
                skin.Apply(this);
            }
        }

        /// <summary>
        /// 应用指定名字的皮肤
        /// </summary>
        /// <param name="skinname"></param>
        public void ApplySkin(string skinname)
        {
            if (skins.TryGetValue(skinname, out var skin))
            {
                skin.Apply(this);
            }
        }

        /// <summary>
        /// 移除皮肤
        /// </summary>
        /// <param name="skinname"></param>
        protected void RemoveSkin(string skinname)
        {
            if (skins.TryGetValue(skinname, out var skin))
            {
                skin.Destroy();
                skins.Remove(skinname);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected override void OnDestroy()
        {
            CPixelPool.Instance.Release(pixels);
            while(skins.Keys.Count > 0)
            {
                var key = skins.Keys.First<string>();
                skins.Remove(key);
            }

            base.OnDestroy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override CRenderableObject Clone()
        {
            var clone = new CRenderableObject(layer)
            {
                name = name,
                enabled = enabled,
            };
            clone.transform.localposition = transform.localposition;

            foreach (var pixel in pixels)
            {
                var clonepixel = CPixelPool.Instance.Alloc(pixel.x, pixel.y, pixel.c, pixel.color);
                clone.pixels.Add(clonepixel);
            }

            foreach (var skin in skins.Values)
            {
                var cloneskin = skin.Clone();
                clone.AddSkin(cloneskin);
            }

            foreach (var child in children)
            {
                var clonechild = child.Clone();
                clone.Add(clonechild);
            }

            return clone;
        }
    }
}
