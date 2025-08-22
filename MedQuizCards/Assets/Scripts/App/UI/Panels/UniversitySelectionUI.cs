using Nato.StateMachine;
using Nato.UI;
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
    public class UniversitySelectionUI : BaseUI
    {
        [field: SerializeField] public UniversityData[] UniversitiesData;

        [field: SerializeField] public UniversityButton UniversityButtonPrefab;
        [field: SerializeField] public List<UniversityButton> UniversityButtons = new List<UniversityButton>();


        [SerializeField] private Transform verticalContainer;

        [field: SerializeField] public Button RankingButton { get; private set; }
        [field: SerializeField] public Button BackButton { get; private set; }
        [field: SerializeField] public TMP_InputField FilterInputField { get; private set; }


        [field: SerializeField] public ScrollRect ScrollRectUniversities { get; private set; }

        public List<UniversityRanking> UniversityRankings;


        protected override void Awake()
        {
            base.Awake();
        }


        public override void Enable()
        {
            base.Enable();
            ShowUniversities();
            BackButton.onClick.AddListener(OnClickBackButton);
            RankingButton.onClick.AddListener(OnClickRankingButton);
            FilterInputField.onValueChanged.AddListener(delegate { OnFilterChanged(FilterInputField); });
        }

        public override void Disable()
        {
            base.Disable();
            BackButton.onClick.RemoveListener(OnClickBackButton);
            RankingButton.onClick.RemoveListener(OnClickRankingButton);
            FilterInputField.onValueChanged.RemoveAllListeners();
        }

        private void OnClickBackButton()
        {
            UIUFSelectionState uFSelectionState = UIStates.Instance.UFSelectionState;
            UIStateManager.Instance.StateMachine.TransitionTo(uFSelectionState);
        }

        private void OnFilterChanged(TMP_InputField inputField)
        {
            string filterText = inputField.text.Trim().ToLower();

            foreach (var uniButton in UniversityButtons)
            {
                string uniName = uniButton.UniversityRanking.CompleteName.ToLower();

                bool show = string.IsNullOrEmpty(filterText) || uniName.StartsWith(filterText) || uniName.Contains(filterText);
                uniButton.gameObject.SetActive(show);
            }

            //StartCoroutine(SnapToTopWhenLayoutSettles());
        }

        private void OnClickRankingButton()
        {
            LoadingPopUp.Instance?.Show();
            StartCoroutine(TransitionToNewState());
        }

        private IEnumerator TransitionToNewState()
        {
            yield return new WaitForSeconds(0.3f);
            UIRankingState rankingState = UIStates.Instance.RankingState;
            UIStateManager.Instance.StateMachine.TransitionTo(rankingState);
            rankingState.Manager.UIPanels.RankingUI.BackButton.onClick.AddListener(() =>
            {
                UIUniversitySelectionState universitySelectionState = UIStates.Instance.UniversitySelectionState;
                UIStateManager.Instance.StateMachine.TransitionTo(universitySelectionState);
            });
        }


        public void ShowUniversities()
        {
            for(int i = UniversityButtons.Count - 1; i >= 0; i--)
            {
                Destroy(UniversityButtons[i].gameObject);
            }
            UniversityButtons.Clear();

            LoadingPopUp.Instance?.Show();
            UniversityRankings = QuizManager.Instance.GetUniversitiesByUF(QuizManager.Instance.CurrentUF);
            for (int i = 0; i < UniversityRankings.Count; i++)
            {
                UniversityRanking universityRanking = UniversityRankings[i];
                UniversityButton universityButton = Instantiate(UniversityButtonPrefab, verticalContainer);
                universityButton.Setup(universityRanking);
                UniversityButtons.Add(universityButton);
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
