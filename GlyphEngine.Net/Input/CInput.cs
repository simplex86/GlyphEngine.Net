namespace GlyphEngine
{
    /// <summary>
    /// 输入
    /// </summary>
    public static class CInput
    {
        private static CKeyboardListener keyboard = new CKeyboardListener();

        /// <summary>
        /// 轮询输入事件
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public static bool Poll(out CKeyboardEvent evt)
        {
            return keyboard.Poll(out evt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        internal static void Update(float dt)
        {
            keyboard.Update(dt);
        }
    }
}
