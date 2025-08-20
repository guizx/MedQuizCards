using Nato.StateMachine;
using Nato.UI;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class RankingUI : BaseUI
    {
        [field: SerializeField] public Button BackButton { get; private set; }

        [SerializeField] private Transform verticalContainer;

        [field: SerializeField] public RankingLeaderboard RankingLeaderboardPrefab { get; private set; }
        private List<RankingLeaderboard> rankings = new List<RankingLeaderboard>();


        [field: SerializeField] public TMP_InputField FilterInputField { get; private set; }
        [field: SerializeField] public ScrollRect ScrollRectUniversities { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            ShowRankings();
        }

        public override void Enable()
        {
            base.Enable();
            //BackButton.onClick.AddListener(OnClickBackButton);
            //ShowRankings();
            if (rankings.Count > 0)
                UpdateRankings();


            FilterInputField.onValueChanged.AddListener(delegate { OnFilterChanged(FilterInputField); });
        }

        public override void Disable()
        {
            base.Disable();
            BackButton.onClick.RemoveAllListeners();
            FilterInputField.onValueChanged.RemoveAllListeners();

        }

        private void OnFilterChanged(TMP_InputField inputField)
        {
            string filterText = inputField.text.Trim().ToLower();

            foreach (var uniButton in rankings)
            {
                string uniName = uniButton.UniversityRanking.UniversityName.ToLower();

                bool show = string.IsNullOrEmpty(filterText) || uniName.StartsWith(filterText) || uniName.Contains(filterText);
                uniButton.gameObject.SetActive(show);
            }

            //StartCoroutine(SnapToTopWhenLayoutSettles());
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
            LoadingPopUp.Instance?.Show();
            DestroyRankings();

            List<UniversityRanking> orderedRankings = QuizManager.Instance.UniversityRankings
                .OrderByDescending(r => r.Score)
                .ToList();

            int i = 1;
            foreach (UniversityRanking university in orderedRankings)
            {
                RankingLeaderboard ranking = Instantiate(RankingLeaderboardPrefab, verticalContainer);
                ranking.Setup(i, university);
                rankings.Add(ranking);
                i++;
            }

            StartCoroutine(SnapToTopWhenLayoutSettles());
            LoadingPopUp.Instance?.Hide();

        }


        public void UpdateRankings()
        {
            LoadingPopUp.Instance?.Show();
            for (int i = 0; i < QuizManager.Instance.UniversityRankings.Count; i++)
            {
                if (QuizManager.Instance.UniversityRankings[i] == rankings[i].UniversityRanking)
                {
                    rankings[i].UniversityRanking.Score = QuizManager.Instance.UniversityRankings[i].Score;
                }
            }


            List<RankingLeaderboard> orderedRankings = rankings
                .OrderByDescending(r => r.UniversityRanking.Score)
                .ToList();


            for (int k = 0; k < orderedRankings.Count; k++)
            {
                orderedRankings[k].UpdateUI(k);
                orderedRankings[k].transform.SetSiblingIndex(k); 
            }

            StartCoroutine(SnapToTopWhenLayoutSettles());
            LoadingPopUp.Instance?.Hide();
        }




        private IEnumerator SnapToTopWhenLayoutSettles()
        {
            var content = ScrollRectUniversities.content;

            content.pivot = new Vector2(content.pivot.x, 1f);
            content.anchorMin = new Vector2(0f, 1f);
            content.anchorMax = new Vector2(1f, 1f);

            ScrollRectUniversities.StopMovement();
            var prevInertia = ScrollRectUniversities.inertia;
            ScrollRectUniversities.inertia = false;
            if (EventSystem.current) EventSystem.current.SetSelectedGameObject(null);

            float lastH = -1f;
            int safety = 12;
            while (safety-- > 0)
            {
                Canvas.ForceUpdateCanvases();
                LayoutRebuilder.ForceRebuildLayoutImmediate(content);

                float h = content.rect.height;
                if (Mathf.Approximately(h, lastH)) break;
                lastH = h;
                yield return null;
            }

            ScrollRectUniversities.verticalNormalizedPosition = 1f;
            yield return new WaitForEndOfFrame();
            ScrollRectUniversities.verticalNormalizedPosition = 1f;

            var pos = content.anchoredPosition;
            pos.y = 0f;
            content.anchoredPosition = pos;

            ScrollRectUniversities.StopMovement();
            ScrollRectUniversities.inertia = prevInertia;
        }



    }
}
