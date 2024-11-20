using LitJson;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 扩展LitJson
    /// </summary>
    public static class CLitJson
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsBool(this JsonData self, string key, out bool value)
        {
            value = false;

            if (self.ContainsKey(key))
            {
                value = (bool)self[key];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool As(this JsonData self, string key, bool defaultValue)
        {
            return self.AsBool(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsInt(this JsonData self, string key, out int value)
        {
            value = 0;

            if (self.ContainsKey(key))
            {
                value = (int)self[key];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int As(this JsonData self, string key, int defaultValue)
        {
            return self.AsInt(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsUInt(this JsonData self, string key, out uint value)
        {
            value = 0U;

            if (self.ContainsKey(key))
            {
                value = (uint)(int)self[key];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint As(this JsonData self, string key, uint defaultValue)
        {
            return self.AsUInt(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsLong(this JsonData self, string key, out long value)
        {
            value = 0L;

            if (self.ContainsKey(key))
            {
                value = (long)self[key];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long As(this JsonData self, string key, long defaultValue)
        {
            return self.AsLong(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsULong(this JsonData self, string key, out ulong value)
        {
            value = 0UL;

            if (self.ContainsKey(key))
            {
                value = (ulong)(long)self[key];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong As(this JsonData self, string key, ulong defaultValue)
        {
            return self.AsULong(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsString(this JsonData self, string key, out string value)
        {
            value = string.Empty;

            if (self.ContainsKey(key))
            {
                value = (string)self[key];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string As(this JsonData self, string key, string defaultValue)
        {
            return self.AsString(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static EBorderStyle AsBorderStyle(this JsonData self, string key, EBorderStyle defaultValue)
        {
            return self.AsInt(key, out var value) ? (EBorderStyle)value: defaultValue;
        }
    }
}
