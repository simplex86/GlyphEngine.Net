using System.Reflection;

namespace SimpleX.CEngine
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
        protected CRenderableObject()
            : this((ulong)ERenderLayer.Default)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        protected CRenderableObject(ERenderLayer layer)
            : this((ulong)layer)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        protected CRenderableObject(ulong layer)
        {
            this.layer = layer;
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
        protected void AddPixel(CPixel pixel)
        {
            pixels.Add(pixel);
        }

        /// <summary>
        /// 清空像素
        /// </summary>
        protected void ClearPixels()
        {
            pixels.Clear();
        }

        /// <summary>
        /// 加载皮肤
        /// </summary>
        public void LoadSkins()
        {
            var types = ReflectionHelper.FindAll<CSkin, CSkinOfAttribute>();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<CSkinOfAttribute>();
                if (attr != null && attr.Is(GetType()))
                {
                    var skin = Activator.CreateInstance(type) as CSkin;
                    AddSkin(skin, attr.applied);
                }
            }
        }

        /// <summary>
        /// 添加皮肤
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skin"></param>
        protected void AddSkin(CSkin skin, bool apply = false)
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
        /// <param name="skinName"></param>
        public void ApplySkin(string skinName)
        {
            if (skins.TryGetValue(skinName, out var skin))
            {
                skin.Apply(this);
            }
        }

        /// <summary>
        /// 移除皮肤
        /// </summary>
        /// <param name="skinName"></param>
        protected void RemoveSkin(string skinName)
        {
            skins.Remove(skinName);
        }
    }
}
