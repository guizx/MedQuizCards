using Nato.StateMachine;
using UnityEngine;

namespace MedQuizCards
{
    public class UIMainScreenState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            Manager.UIPanels.MainScreenUI.Enable();
            QuizManager.Instance?.EnableDecks();
            Deck.IsClicklable = true;
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.MainScreenUI.Disable();
            //QuizManager.Instance?.DisableDecks();
            Deck.IsClicklable = false;

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
