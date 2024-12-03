namespace SimpleX.CEngine
{
    /// <summary>
    /// 不会被销毁的场景，引擎启动后被自动加载
    /// </summary>
    internal class CDontDestroyScene : CScene
    {
        /// <summary>
        /// 
        /// </summary>
        public CDontDestroyScene()
            : base(string.Empty)
        {
            InitUI();
        }

        /// <summary>
        /// 初始化UI相关的资源
        /// </summary>
        private void InitUI()
        {
            // 添加UI相机
            var camera = new CCamera("UICamera", uint.MaxValue)
            {
                mask = (ulong)ERenderMask.UI,
            };
            Add(camera);
        }
    }
}
