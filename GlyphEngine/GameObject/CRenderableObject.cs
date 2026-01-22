using System;
using System.Collections.Generic;
using System.Linq;

namespace GlyphEngine
{
    /// <summary>
    /// 可渲染对象
    /// </summary>
    internal class CRenderableObject : CGameObject, IRenderable, ISkinable
    {
        /// <summary>
        /// 渲染层级
        /// </summary>
        public ulong Layer { get; }

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
        internal CRenderableObject(ERenderLayer layer)
            : this(layer, null)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="owner"></param>
        internal CRenderableObject(ERenderLayer layer, IGameObjectOwner owner)
            : this(0, 0, layer, owner)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layer"></param>
        /// <param name="scene"></param>
        internal CRenderableObject(int x, int y, ERenderLayer layer)
            : this(x, y, layer, null)
        {
            this.Layer = (ulong)layer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layer"></param>
        /// <param name="owner"></param>
        internal CRenderableObject(int x, int y, ERenderLayer layer, IGameObjectOwner owner)
            : base(x, y, owner)
        {
            Layer = (ulong)layer;
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
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal CPixel GetPixel(int index)
        {
            return pixels[index];
        }

        /// <summary>
        /// 清空像素
        /// </summary>
        internal void ClearPixels()
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
            if (skins.TryGetValue(skin.Name, out var _))
            {
                skins[skin.Name] = skin;
            }
            else
            {
                skins.Add(skin.Name, skin);
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
        protected override void OnDestroy()
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
        internal protected override CRenderableObject Clone()
        {
            return Clone(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        internal protected override CRenderableObject Clone(IGameObjectOwner owner)
        {
            var clone = new CRenderableObject((ERenderLayer)Layer, owner)
            {
                Name = Name,
                Enabled = Enabled,
            };
            clone.Transform.LocalPosition = Transform.LocalPosition;

            foreach (var pixel in pixels)
            {
                var clonepixel = CPixelPool.Instance.Alloc(pixel.X, pixel.Y, pixel.Glyph, pixel.Color);
                clone.pixels.Add(clonepixel);
            }

            foreach (var skin in skins.Values)
            {
                var cloneskin = skin.Clone();
                clone.AddSkin(cloneskin);
            }

            foreach (var child in Children)
            {
                var clonechild = child.Clone();
                clone.Add(clonechild);
            }

            return clone;
        }
    }
}
