using Nato.StateMachine;
using Nato.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class RankingUI : BaseUI
    {
        [field: SerializeField] public Button BackButton { get; private set; }

        [SerializeField] private Transform verticalContainer;

        [field: SerializeField] public RankingLeaderboard RankingLeaderboardPrefab { get; private set; }
        private List<RankingLeaderboard> rankings = new List<RankingLeaderboard>();

        public override void Enable()
        {
            base.Enable();
            //BackButton.onClick.AddListener(OnClickBackButton);
            ShowRankings();   
        }

        public override void Disable()
        {
            base.Disable();
            BackButton.onClick.RemoveAllListeners();
        }

        private void OnClickBackButton()
        {
            UIMainScreenState mainScreenState = UIStates.Instance.MainScreenState;
            UIStateManager.Instance.StateMachine.TransitionTo(mainScreenState);
        }

        private void DestroyRankings()
        {
            for(int i = rankings.Count - 1; i >= 0; i--)
            {
                Destroy(rankings[i].gameObject);
            }
            rankings.Clear();
        }

        public void ShowRankings()
        {
            DestroyRankings();

            List<UniversityRanking> orderedRankings = QuizManager.Instance.UniversityRankings
                .OrderByDescending(r => r.Score)
                .ToList();

            foreach (UniversityRanking university in orderedRankings)
            {
                RankingLeaderboard ranking = Instantiate(RankingLeaderboardPrefab, verticalContainer);
                ranking.Setup(university.UniversityData.Icon, university.Score);
                rankings.Add(ranking);  
            }
        }

    }
}
