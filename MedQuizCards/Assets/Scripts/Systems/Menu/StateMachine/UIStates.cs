using MedQuizCards;
using Nato.Singleton;
using UnityEngine;

namespace Nato.StateMachine
{
    public class UIStates : Singleton<UIStates>
    {
        [field: Header("Template States")]
        [field: SerializeField] public UIUniversitySelectionState UniversitySelectionState { get; private set; }
        [field: SerializeField] public UIMainScreenState MainScreenState { get; private set; }
        [field: SerializeField] public UIQuestionState QuestionState { get; private set; }
        [field: SerializeField] public UIRankingState RankingState { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            //Global
            UniversitySelectionState = new UIUniversitySelectionState();
            QuestionState = new UIQuestionState();
            MainScreenState = new UIMainScreenState();
            RankingState = new UIRankingState();
        }

        private void OnDestroy()
        {
            //Global
            UniversitySelectionState.OnDestroyTick();
            MainScreenState.OnDestroyTick();
            QuestionState.OnDestroyTick();
            RankingState.OnDestroyTick();
        }
    }
}
