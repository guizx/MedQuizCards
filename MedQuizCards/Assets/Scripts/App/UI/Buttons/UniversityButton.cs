using Nato.StateMachine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    [RequireComponent(typeof(Button))]
    public class UniversityButton : MonoBehaviour
    {
        public UniversityRanking UniversityRanking { get; private set; }
        public Button Button { get; private set; }
        [field: SerializeField] public TextMeshProUGUI UniversityNameText { get; private set; }
        [field: SerializeField] public Image UniversityIconImage { get; private set; }


        private void OnEnable()
        {
            if(Button == null)
                Button = GetComponent<Button>();    

            Button.onClick.AddListener(OnClickButton);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            QuizManager.Instance.SetCurrentUniversity(UniversityRanking);
            UIMainScreenState mainScreenState = UIStates.Instance.MainScreenState;
            UIStateManager.Instance.StateMachine.TransitionTo(mainScreenState);
        }

        public void Setup(UniversityRanking university)
        {
            UniversityRanking = university;
            UniversityNameText.SetText(university.UniversityData.Name);
            UniversityIconImage.sprite = university.UniversityData.Icon;
        }
    }
}
