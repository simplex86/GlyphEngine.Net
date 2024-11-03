using SimpleX.CEngine.UI;

namespace SimpleX.CEngine
{
    /// <summary>
    /// 永存的场景，引擎启动后该场景就会被自动加载
    /// </summary>
    internal class CPermanentScene : CScene
    {
        public CPermanentScene()
        {
            // 添加UI相机
            var camera = new CCamera("UICamera");
            camera.mask = (ulong)ERenderMask.UI;
            Add(camera);
            // 添加UI根节点
            var uiroot = new CGameObject();
            uiroot.name = "UIRoot";
            Add(uiroot);

            // 设置UI根节点
            CUIManager.SetRoot(uiroot);
        }
    }
}
