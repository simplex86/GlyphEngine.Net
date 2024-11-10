using SimpleX.CEngine.UI;

namespace CExample
{
    internal class LaunchUI : CPanel<LaunchUIView>
    {
        public LaunchUI()
        {
            view.startButton.AddClick(OnStartButtonClickedHandler);
        }

        private void OnStartButtonClickedHandler()
        {
            ProcedureManager.ChangeTo<SnakeProcedure>();
        }
    }
}
