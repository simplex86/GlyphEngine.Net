using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public static class CResourceManager
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
            if (!gcatch.TryGetValue(filepath, out var gameobject))
            {
                gameobject = CGameObjectDeserializer.Deserialize(filepath);
                gcatch.Add(filepath, gameobject);
            }

            var instance = gameobject.Clone();
            instance.transform.position = new Vector2(x, y);

            if (parent == null)
            {
                var scene = CSceneManager.GetMainScene();
                scene.Add(instance);
            }
            else
            {
                parent.Add(instance);
            }

            return instance;
        }

        /// <summary>
        /// 加载纹理
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CTexture Load(string filepath, bool transparent)
        {
            var key = $"{filepath}.{transparent}";
            if (tcatch.TryGetValue(key, out var tex))
            {
                return tex;
            }

            tex = new CTexture(transparent);
            try
            {
                var lines = File.ReadAllLines($"{CPath.resourcesPath}/{filepath}");
                if (lines.Length > 0)
                {
                    tex.width = 0;
                    foreach (var line in lines)
                    {
                        tex.width = Math.Max(tex.width, line.Length);
                    }
                    tex.height = lines.Length;

                    tex.chars = new List<char>(tex.width * tex.height);
                    foreach (var line in lines)
                    {
                        for (int i = 0; i < tex.width; i++)
                        {
                            var c = (i < line.Length) ? line[i] : CChar.Space;
                            if (tex.transparent && c == CChar.Space)
                            {
                                c = CChar.Empty;
                            }
                            tex.chars.Add(c);
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
        /// 加载文本
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string LoadText(string filepath)
        {
            return File.ReadAllText($"{CPath.resourcesPath}/{filepath}");
        }

        /// <summary>
        /// 加载json
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static JsonData LoadJson(string filepath)
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
        public static T LoadJson<T>(string filepath)
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
                CGameObject.Destroy(gameobject);
            }
            gcatch.Clear();

            // 清空texture缓存
            tcatch.Clear();
        }
    }
}
