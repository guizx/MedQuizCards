using Nato.StateMachine;
using Nato.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class UFSelectionUI : BaseUI
    {

        [field: SerializeField] public UFButton UFButtonPrefab;
        [field: SerializeField] public List<UFButton> UFButtons = new List<UFButton>();


        [SerializeField] private Transform verticalContainer;

        [field: SerializeField] public TMP_InputField FilterInputField { get; private set; }
        [field: SerializeField] public ScrollRect ScrollRectUniversities { get; private set; }
        [field: SerializeField] public Button BackButton { get; private set; }


        protected override void Awake()
        {
            base.Awake();
        }


        public override void Enable()
        {
            base.Enable();
            FilterInputField.onValueChanged.AddListener(delegate { OnFilterChanged(FilterInputField); });
            BackButton.onClick.AddListener(OnClickBackButton);
        }

        public override void Disable()
        {
            base.Disable();
            FilterInputField.onValueChanged.RemoveAllListeners();
            BackButton.onClick.RemoveListener(OnClickBackButton);
        }

        private void OnFilterChanged(TMP_InputField inputField)
        {
            string filterText = inputField.text.Trim().ToLower();

            foreach (var uniButton in UFButtons)
            {
                string uniName = uniButton.UF.ToLower();

                bool show = string.IsNullOrEmpty(filterText) || uniName.StartsWith(filterText) || uniName.Contains(filterText);
                uniButton.gameObject.SetActive(show);
            }

            //StartCoroutine(SnapToTopWhenLayoutSettles());
        }

     

        private void OnClickBackButton()
        {
            UITitleScreenState titleScreenState = UIStates.Instance.TitleScreenState;
            UIStateManager.Instance.StateMachine.TransitionTo(titleScreenState);
        }


        private void Start()
        {
            LoadingPopUp.Instance?.Show();

            List<string> ufList = QuizManager.Instance.UniversitiesUFs.Distinct().ToList();
            for (int i = 0; i < ufList.Count; i++)
            {
                UFButton universityButton = Instantiate(UFButtonPrefab, verticalContainer);
                universityButton.Setup(ufList[i]);
                UFButtons.Add(universityButton);
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
