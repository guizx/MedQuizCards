using Nato.StateMachine;
using UnityEngine;

namespace MedQuizCards
{
    public class UIUniversityCreateState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            Manager.UIPanels.UniversityCreateUI.Enable();
            Deck.IsClicklable = false;
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.UniversityCreateUI.Disable();
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
