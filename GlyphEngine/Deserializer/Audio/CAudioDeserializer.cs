using LitJson;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CAudioDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CAudioSource Deserialize(string filepath)
        {
            var data = CResources.LoadJson(filepath);
            return Deserialize(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static CAudioSource Deserialize(JsonData data)
        {
            var filepath = data.As("filepath", string.Empty);
            if (!string.IsNullOrEmpty(filepath))
            {
                filepath = CPath.Combine(CPath.Resources, filepath);

                var volume = data.As("volume", 1.0f);
                var loop = data.As("loop", false);

                return new CAudioSource(filepath, volume, loop);
            }

            return null;
        }
    }
}
