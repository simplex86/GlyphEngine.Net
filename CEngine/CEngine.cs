using System.Text;
using CEngine.UI;
using CEngine.Input;

namespace CEngine
{
    /// <summary>
    /// 基于控制台的简易2D游戏引擎
    /// </summary>
    public class CEngine
    {
        /// <summary>
        /// 
        /// </summary>
        private IApplication application = null;
        /// <summary>
        /// 
        /// </summary>
        private bool running = false;

        /// <summary>
        /// 
        /// </summary>
        public CEngine(string title)
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

            CSceneManager.Init();
            CEventManager.Init();

            var type = CReflectionHelper.Find<IApplication, ApplicationEntryAttribute>();
            if (type != null)
            {
                application = Activator.CreateInstance(type) as IApplication;
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
                application?.Start();

                while (running)
                {
                    var dt = CTime.Update();
                    CKeyboard.Update(dt);
                    application?.Update(dt);
                    CSceneManager.Update(dt);
                    CUIManager.Update(dt);
                }
            }
            catch (Exception ex)
            {
                CDebug.Error(ex.ToString());
            }
            finally
            {
                application?.Exit();
                application = null;

                CTime.Stop();
            }
        }
    }
}