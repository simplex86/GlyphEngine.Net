using System;
using System.Text;

namespace GlyphEngine
{
    /// <summary>
    /// 基于控制台的简易2D游戏引擎
    /// </summary>
    public sealed class GlyphEngine
    {
        /// <summary>
        /// 
        /// </summary>
        private IEngineEntry entry = null;
        /// <summary>
        /// 
        /// </summary>
        private bool running = false;

        /// <summary>
        /// 
        /// </summary>
        public GlyphEngine(string title)
        {
            // 隐藏光标
            Console.CursorVisible = false;
            // 设置编码
            Console.OutputEncoding = Encoding.Unicode;
            // 设置标题
            Console.Title = title;
        }

        /// <summary>
        /// 获取分辨率
        /// </summary>
        /// <returns></returns>
        public (int x, int y) GetResolution()
        {
            return (Console.BufferWidth, Console.BufferHeight);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            running = true;

            CWorld.Init();
            CWindows.Init();

            var type = CReflectionHelper.Find<IEngineEntry, CEngineEntryAttribute>();
            if (type != null)
            {
                entry = Activator.CreateInstance(type) as IEngineEntry;
            }

            Loop();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// 主循环
        /// </summary>
        private void Loop()
        {
            try
            {
                CTime.Start();
                entry?.Start();

                while (running)
                {
                    var dt = CTime.Update();
                    CInput.Update(dt);
                    entry?.Update(dt);
                    CWorld.Update(dt);
                    CWindows.Update(dt);
                }
            }
            catch (Exception ex)
            {
                CDebug.Error(ex.ToString());
            }
            finally
            {
                entry?.Exit();
                entry = null;

                CTime.Stop();
            }
        }
    }
}