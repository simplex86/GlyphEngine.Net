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
        public static CTexture LoadTextrue(string filepath)
        {
            var tex = new CTexture();
            try
            {
                var lines = File.ReadAllLines(filepath);
                if (lines != null && lines.Length > 0)
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
                            var c = (i < line.Length) ? line[i] : CChar.Empty;
                            tex.chars.Add(c);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CDebug.Error($"load texture failed. filepath = {filepath}.\n{ex}");
            }

            return tex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="path"></param>
        public static void LoadTexture(this CTexture tex, string path)
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
                            var c = (i < line.Length) ? line[i] : CChar.Empty;
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
