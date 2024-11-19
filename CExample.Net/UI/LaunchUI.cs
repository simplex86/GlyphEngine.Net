using SimpleX.CEngine.UI;

namespace CExample
{
    [CUIPanel("UI/LaunchUI.uidesign")]
    internal class LaunchUI : CUIPanel
    {
        public LaunchUI()
        {
            var startButton = GetComponent<CUIButton>("start");
            startButton.AddClick(OnStartButtonClickedHandler);
        }

        private void OnStartButtonClickedHandler()
        {
            ProcedureManager.ChangeTo<SnakeProcedure>();
        }
    }
}
