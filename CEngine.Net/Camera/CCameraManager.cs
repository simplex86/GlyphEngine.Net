using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 相机管理器
    /// </summary>
    public static class CCameraManager
    {
        /// <summary>
        /// 渲染缓存
        /// </summary>
        private static CRenderBuffer buffer = new CRenderBuffer();
        /// <summary>
        /// <tag, camera>
        /// </summary>
        private static Dictionary<string, CCamera> cameras = new Dictionary<string, CCamera>();

        internal static CCamera Create(string tag, int order = 0)
        {
            if (cameras.TryGetValue(tag, out var _))
            {
                return null;
            }

            var camera = new CCamera(tag, order, buffer);
            cameras.Add(tag, camera);

            return camera;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        internal static void Destroy(string tag)
        {
            cameras.Remove(tag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="camera"></param>
        internal static void Destroy(CCamera camera)
        {
            if (camera != null)
            {
                cameras.Remove(camera.Tag);
            }
        }

        internal static void Update()
        {
            buffer.Render();
        }

        /// <summary>
        /// 获取指定tag的相机
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool Get(string tag, out CCamera camera)
        {
            return cameras.TryGetValue(tag, out camera);
        }
    }
}
