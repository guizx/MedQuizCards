using Nato.StateMachine;
using UnityEngine;

namespace MedQuizCards
{
    public class UIRankingState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            QuizManager.Instance.DisableDecks();
            Manager.UIPanels.RankingUI.Enable();
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.RankingUI.Disable();
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
