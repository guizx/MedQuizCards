using Nato.StateMachine;

namespace MedQuizCards
{
    public class UIUniversitySelectionState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            Manager.UIPanels.UniversitySelectionUI.Enable();
            QuizManager.Instance.DisableDecks();
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.UniversitySelectionUI.Disable();
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
