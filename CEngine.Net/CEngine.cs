using System.Text;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 基于控制台的简易2D游戏引擎
    /// </summary>
    public static class CEngine
    {
        private static IApplication application = null;
        private static bool running = false;

        /// <summary>
        /// 设置分辨率
        /// 注：引擎启动后设置无效
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void SetResolution(int width, int height)
        {
            if (running) return;

            try
            {
                Console.BufferWidth = width;
                Console.WindowWidth = width;
                Console.BufferHeight = height;
                Console.WindowHeight = height;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 获取分辨率
        /// </summary>
        /// <returns></returns>
        public static (int width, int height) GetResolution()
        {
            return (Console.BufferWidth, Console.BufferHeight);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            // 隐藏控制台的光标
            Console.CursorVisible = false;
            // 
            Console.OutputEncoding = Encoding.Unicode;

            CSceneManager.Start();
            CTime.Start();

            var type = ReflectionHelper.Find<IApplication, ApplicationEntryAttribute>();
            if (type != null)
            {
                application = Activator.CreateInstance(type) as IApplication;
                application.Start();
            }

            running = true;
            Loop();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            application?.Exit();
            application = null;

            running = false;
            CTime.Stop();
        }

        /// <summary>
        /// 主循环
        /// </summary>
        private static void Loop()
        {
            try
            {
                while (running)
                {
                    CTime.Update();
                    CInput.Update();

                    var dt = CTime.deltatime;
                    application?.Update(dt);

                    CSceneManager.Update();
                    CCameraManager.Update();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}