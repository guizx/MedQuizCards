using Nato.StateMachine;

namespace MedQuizCards
{
    public class UIQuestionState : BaseState<UIStateManager>
    {
        public override void OnStart(UIStateManager manager)
        {
            base.OnStart(manager);
            Manager.UIPanels.QuestionUI.Enable();
            Manager.UIPanels.QuestionUI.DisableResultPopUp();
            Deck.IsClicklable = false;
        }
        public override void OnEnd()
        {
            base.OnEnd();
            Manager.UIPanels.QuestionUI.Disable();
            QuizManager.Instance?.DisableDecks();
            Deck.IsClicklable = true;
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
