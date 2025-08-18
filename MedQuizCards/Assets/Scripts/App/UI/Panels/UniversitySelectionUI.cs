using Nato.StateMachine;
using Nato.UI;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class UniversitySelectionUI : BaseUI
    {
        [field: SerializeField] public UniversityData[] UniversitiesData;

        [field: SerializeField] public UniversityButton UniversityButtonPrefab;


        [SerializeField] private Transform verticalContainer;

        [field: SerializeField] public Button RankingButton { get; private set; }

        public override void Enable()
        {
            base.Enable();
            RankingButton.onClick.AddListener(OnClickRankingButton);
        }

        public override void Disable()
        {
            base.Disable();
            RankingButton.onClick.AddListener(OnClickRankingButton);

        }

        private void OnClickRankingButton()
        {
            UIRankingState rankingState = UIStates.Instance.RankingState;
            UIStateManager.Instance.StateMachine.TransitionTo(rankingState);
            rankingState.Manager.UIPanels.RankingUI.BackButton.onClick.AddListener(() =>
            {
                UIUniversitySelectionState universitySelectionState = UIStates.Instance.UniversitySelectionState;
                UIStateManager.Instance.StateMachine.TransitionTo(universitySelectionState);
            });
        }


        private void Start()
        {
            for(int i = 0; i < QuizManager.Instance.UniversityRankings.Length; i++)
            {
                UniversityRanking universityRanking = QuizManager.Instance.UniversityRankings[i];
                UniversityButton universityButton = Instantiate(UniversityButtonPrefab, verticalContainer);
                universityButton.Setup(universityRanking);
            }
        }
    }
}
