using System;
using System.Collections.Generic;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 相机管理器
    /// </summary>
    public static class CCameraManager
    {
        private static CSceneManagerImp cSceneManagerImp = null;

        /// <summary>
        /// 初始化
        /// </summary>
        internal static void SetSceneManagerImp(CSceneManagerImp sceneManagerImp)
        {
            cSceneManagerImp = sceneManagerImp;
        }

        /// <summary>
        /// 获取指定名字的相机
        /// </summary>
        /// <param name="name"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static bool Find(string name, out CCamera camera)
        {
            camera = null;

            foreach (var cam in cSceneManagerImp.cameras)
            {
                if (cam.Name == name)
                {
                    camera = cam;
                    return true;
                }
            }

            return false;
        }
    }
}
