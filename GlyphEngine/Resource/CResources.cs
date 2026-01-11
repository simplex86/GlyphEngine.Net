using System;
using System.IO;
using System.Collections.Generic;
using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public static class CResources
    {
        /// <summary>
        /// gameobject缓存
        /// </summary>
        private static Dictionary<string, CGameObject> gcatch = new Dictionary<string, CGameObject>();
        /// <summary>
        /// texture缓存
        /// </summary>
        private static Dictionary<string, CTexture> tcatch = new Dictionary<string, CTexture>();

        /// <summary>
        /// 加载gameobject
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath)
        {
            return Load(filepath, 0, 0);
        }

        /// <summary>
        /// 加载gameobject
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath, int x, int y)
        {
            return Load(filepath, x, y, null);
        }

        /// <summary>
        /// 加载gameobject
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath, int x, int y, CGameObject parent)
        {
            return Load(filepath, x, y, parent, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="parent"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static CGameObject Load(string filepath, int x, int y, CGameObject parent, IGameObjectOwner owner)
        {
            if (!gcatch.TryGetValue(filepath, out var prototype))
            {
                prototype = CGameObjectDeserializer.Deserialize(filepath);
                gcatch.Add(filepath, prototype);
            }

            var gameobject = prototype.Clone(owner);
            gameobject.Transform.LocalPosition = new CVector2(x, y);

            if (parent != null)
            {
                parent.Add(gameobject);
            }
            else
            {
                CWorld.Add(gameobject);
            }

            return gameobject;
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CScene LoadScene(string filepath)
        {
            var scene = CSceneDeserializer.Deserialize(filepath);
            CWorld.Add(scene);

            return scene;
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="scene"></param>
        public static void UnloadScene(CScene scene)
        {
            scene.Destroy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CPanel LoadUI(string filepath, Type type)
        {
            var panel = CPanelDeserializer.Deserialize(filepath, type);
            CWindows.Add(panel);

            return panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        public static void UnloadUI(CPanel panel)
        {
            panel.Destroy();
        }

        /// <summary>
        /// 加载纹理
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CTexture LoadTex(string filepath, bool transparent)
        {
            var key = $"{filepath}.{transparent}";
            if (tcatch.TryGetValue(key, out var tex))
            {
                tex.Refrence();
                return tex;
            }

            tex = new CTexture(transparent);
            try
            {
                var lines = File.ReadAllLines($"{CPath.Resources}/{filepath}");
                if (lines.Length > 0)
                {
                    tex.Width = 0;
                    foreach (var line in lines)
                    {
                        tex.Width = Math.Max(tex.Width, line.Length);
                    }
                    tex.Height = lines.Length;

                    tex.Glyphs = new List<char>(tex.Width * tex.Height);
                    foreach (var line in lines)
                    {
                        for (int i = 0; i < tex.Width; i++)
                        {
                            var c = (i < line.Length) ? line[i] : CChar.Space;
                            if (tex.Transparent && c == CChar.Space)
                            {
                                c = CChar.Empty;
                            }
                            tex.Glyphs.Add(c);
                        }
                    }
                }
                tcatch.Add(key, tex);

                return tex;
            }
            catch (Exception ex)
            {
                CDebug.Error($"load texture failed. filepath = {filepath}.\n{ex}");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CAudioSource LoadAudio(string filepath)
        {
            return CAudioDeserializer.Deserialize(filepath);
        }

        /// <summary>
        /// 加载文本
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string LoadText(string filepath)
        {
            return File.ReadAllText($"{CPath.Resources}/{filepath}");
        }

        /// <summary>
        /// 加载json
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        internal static JsonData LoadJson(string filepath)
        {
            try
            {
                var json = LoadText(filepath);
                return JsonMapper.ToObject(json);
            }
            catch (Exception ex)
            {
                CDebug.Error(ex.ToString());
            }

            return null;
        }

        /// <summary>
        /// 加载json并转换成指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <returns></returns>
        internal static T LoadJson<T>(string filepath)
        {
            try
            {
                var json = LoadText(filepath);
                return JsonMapper.ToObject<T>(json);
            }
            catch (Exception ex)
            {
                CDebug.Error(ex.ToString());
            }

            return default(T);
        }

        /// <summary>
        /// 卸载所有
        /// </summary>
        public static void UnloadAll()
        {
            // 清空gameobject缓存
            foreach (var gameobject in gcatch.Values)
            {
                gameobject.Destroy();
            }
            gcatch.Clear();
        }
    }
}
