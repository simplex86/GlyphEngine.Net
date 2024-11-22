using LitJson;
using System;

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
        public static bool AsChar(this JsonData self, string key, out char value)
        {
            value = CChar.Empty;

            if (self.ContainsKey(key))
            {
                var node = self[key];
                if (node.IsInt)
                {
                    value = (char)(int)node;
                }
                else if (node.IsString)
                {
                    var str = (string)node;
                    value = (str.Length > 0) ? str[0] : CChar.Empty;
                }
                else
                {
                    value = CChar.Empty;
                }
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
        public static char As(this JsonData self, string key, char defaultValue)
        {
            return self.AsChar(key, out var value) ? value : defaultValue;
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
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AsEnum<TEnum>(this JsonData self, string key, out TEnum value) where TEnum : Enum
        {
            // 获取枚举的基础类型
            var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));

            if (underlyingType == typeof(int))
            {
                if (self.AsInt(key, out var val)) return ToEnum(val, out value);
            }
            else if (underlyingType == typeof(uint))
            {
                if (self.AsUInt(key, out var val)) return ToEnum(val, out value);
            }
            else if (underlyingType == typeof(long))
            {
                if (self.AsLong(key, out var val)) return ToEnum(val, out value);
            }
            else if (underlyingType == typeof(ulong))
            {
                if (self.AsULong(key, out var val)) return ToEnum(val, out value);
            }

            // 将枚举定义的第一个值作为默认值
            value = Enum.GetValues(typeof(TEnum))
                        .Cast<TEnum>()
                        .First();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum As<TEnum>(this JsonData self, string key, TEnum defaultValue) where TEnum : Enum
        {
            return self.AsEnum<TEnum>(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private static bool ToEnum<TEnum, TInput>(TInput input, out TEnum output)
        {
            // 检查值是否在枚举范围内
            if (Enum.IsDefined(typeof(TEnum), input))
            {
                output = (TEnum)Enum.ToObject(typeof(TEnum), input);
                return true;
            }

            // 输出错误信息
            CDebug.Error($"{input} is not valid for {typeof(TEnum).Name}");

            // 将枚举定义的第一个值作为默认值
            output = Enum.GetValues(typeof(TEnum))
                         .Cast<TEnum>()
                         .First();

            return false;
        }
    }
}
