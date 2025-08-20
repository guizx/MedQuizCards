using Nato.StateMachine;
using UnityEngine;

namespace MedQuizCards
{
    public class UIUFSelectionState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            Manager.UIPanels.UFSelectionUI.Enable();
            Deck.IsClicklable = false;
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.UFSelectionUI.Disable();
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
