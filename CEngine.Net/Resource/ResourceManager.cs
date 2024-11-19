using LitJson;
using System.IO;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ResourceManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CTexture LoadTextrue(string filepath, bool transparent)
        {
            var tex = new CTexture(transparent);
            tex.Load(filepath);

            return tex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string LoadText(string filepath)
        {
            return File.ReadAllText($"{CPath.resourcesPath}/{filepath}");
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="path"></param>
        public static void LoadTexture(this CTexture tex, string path, bool transparent)
        {
            try
            {
                var lines = File.ReadAllLines($"{CPath.resourcesPath}/{path}");
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
                            if (transparent && c == CChar.Space)
                            {
                                c = CChar.Empty;
                            }
                            tex.chars.Add(c);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CDebug.Error($"load texture failed. filepath = {path}.\n{ex}");
            }
        }
    }
}
