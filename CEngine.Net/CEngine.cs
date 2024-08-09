using System.Text;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 基于控制台的简易2D游戏引擎
    /// </summary>
    public class CEngine
    {
        private IApplication application = null;

        private CSceneManagerImp sceneManagerImp = null;
        private CTimeImp timeImp = null;
        private CInputImp inputImp = null;

        private bool running = false;

        /// <summary>
        /// 
        /// </summary>
        public CEngine()
        {
            // 隐藏光标
            Console.CursorVisible = false;
            // 设置编码
            Console.OutputEncoding = Encoding.Unicode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public CEngine(int width, int height)
            : this()
        {
            Console.WindowWidth = width;
            Console.BufferWidth = Console.WindowLeft + width;
            Console.WindowHeight = height;
            Console.BufferHeight = Console.WindowTop + height;
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

            sceneManagerImp = new CSceneManagerImp();
            CSceneManager.SetSceneManagerImp(sceneManagerImp);
            CCameraManager.SetSceneManagerImp(sceneManagerImp);

            timeImp = new CTimeImp();
            CTime.SetTimeImp(timeImp);

            inputImp = new CInputImp();
            CInput.SetTimeImp(inputImp);

            var type = ReflectionHelper.Find<IApplication, ApplicationEntryAttribute>();
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
                timeImp.Start();
                application?.Start();

                while (running)
                {
                    timeImp.Update();
                    inputImp.Update();
                    application?.Update(timeImp.deltatime);
                    sceneManagerImp.Update();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                application?.Exit();
                application = null;

                timeImp.Stop();
            }
        }
    }
}