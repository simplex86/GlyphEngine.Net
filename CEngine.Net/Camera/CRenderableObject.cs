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
        internal protected CRenderableObject()
            : this(ERenderLayer.Default)
        {

        }

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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layer"></param>
        internal protected CRenderableObject(int x, int y, ERenderLayer layer)
            : base(x, y)
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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void AddPixel(int x, int y)
        {
            var pixel = CPixelPool.Instance.Alloc(x, y);
            AddPixel(pixel);
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

        public void LoadSkin(string filepath)
        {
            CSkinDeserializer.Deserialize(filepath, this);
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
            skins.Remove(skinname);
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected override void OnDestroy()
        {
            CPixelPool.Instance.Release(pixels);
            base.OnDestroy();
        }
    }
}
