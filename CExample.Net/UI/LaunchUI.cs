using SimpleX.CEngine.UI;
using SimpleX.CEngine.Input;

namespace CExample
{
    internal class LaunchUI : CPanel<LaunchUIView>
    {
        public override void Update(float dt)
        {
            if (CKeyboard.Poll(out var evt))
            {
                if (evt.type == EKeyboardEventType.Up &&
                    evt.keycode == (int)ConsoleKey.Spacebar)
                {
                    ProcedureManager.ChangeTo<SnakeProcedure>();
                }
            }
        }
    }
}
