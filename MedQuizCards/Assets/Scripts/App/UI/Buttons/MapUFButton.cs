using Nato.StateMachine;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class MapUFButton : MonoBehaviour
    {
        public Button Button;
        public string UF;

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClickButton);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClickButton);

        }

        private void OnClickButton()
        {
            QuizManager.Instance.CurrentUF = UF;
            UIUniversitySelectionState universitySelectionState = UIStates.Instance.UniversitySelectionState;
            UIStateManager.Instance.StateMachine.TransitionTo(universitySelectionState);
        }
    }
}
