using Nato.StateMachine;
using UnityEngine;

namespace MedQuizCards
{
    public class UITitleScreenState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            Manager.UIPanels.TitleScreenUI.Enable();
            Deck.IsClicklable = false;
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.TitleScreenUI.Disable();
        }
        public override void OnDestroyTick()
        {
            base.OnDestroyTick();
        }

        public override void OnTick()
        {
            base.OnTick();
        }
    }
}
