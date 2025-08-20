using Nato.StateMachine;
using Nato.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class TitleScreenUI : BaseUI
    {
        [field: SerializeField] public Button StartButton { get; private set; }

        public override void Enable()
        {
            base.Enable();
            StartButton.onClick.AddListener(OnClickStartButton);
        }

        public override void Disable()
        {
            base.Disable();
            StartButton.onClick.RemoveListener(OnClickStartButton);
        }

        private void OnClickStartButton()
        {
            UIUFSelectionState UFSelectionState = UIStates.Instance.UFSelectionState;
            UIStateManager.Instance.StateMachine.TransitionTo(UFSelectionState);
        }
    }
}
