using LitJson;
using System;

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
        private static CAudioClip INVALID_AUDIO_CLIP = new CAudioClip();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static CAudioClip Deserialize(string filepath)
        {
            var data = CResources.LoadJson(filepath);
            return Deserialize(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static CAudioClip Deserialize(JsonData data)
        {
            var filepath = data.As("filepath", string.Empty);
            if (!string.IsNullOrEmpty(filepath))
            {
                filepath = CPath.Combine(CPath.Resources, filepath);

                var volume = data.As("volume", 1.0f);
                var loop = data.As("loop", false);

                return new CAudioClip()
                {
                    Path = filepath,
                    Volume = Math.Clamp(volume, 0.0f, 1.0f),
                    Loop = loop
                };
            }

            return INVALID_AUDIO_CLIP;
        }
    }
}
