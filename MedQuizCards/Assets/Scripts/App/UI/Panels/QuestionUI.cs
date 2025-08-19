using DG.Tweening;
using Nato.StateMachine;
using Nato.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class QuestionUI : BaseUI
    {
        public GameObject QuizCardPanel;
        public GameObject CardFront;
        public GameObject CardBack;

        public TextMeshProUGUI ProcedureTitleText;
        public TextMeshProUGUI QuestionText;

        public AnswerButton[] AnswerButtons;

        public GameObject ResultPopUp;
        public TextMeshProUGUI ResultText;
        public Button ResultButton;

        public GameObject SuccessVFX;

        public Image ProcedureIconImage;

        private void OnEnable()
        {
            ResultButton.onClick.AddListener(OnClickResultButton);
            DisableResultPopUp();
        }

        private void OnDisable()
        {
            ResultButton.onClick.RemoveListener(OnClickResultButton);
        }

        public override void Enable()
        {
            base.Enable();
            for(int i = 0; i < AnswerButtons.Length; i++)
            {
                AnswerButtons[i].Button.onClick.RemoveAllListeners();
            }
        }

        private void OnClickResultButton()
        {
            UIUniversitySelectionState universitySelectionState = UIStates.Instance.UniversitySelectionState;
            UIStateManager.Instance.StateMachine.TransitionTo(universitySelectionState);
            StartCoroutine(EnableClickCoroutine());
        }

        public void Setup(ProcedureDeckData procedureDeck, QuizQuestionData question)
        {
            ProcedureIconImage.sprite = procedureDeck.procedureImage;
            Deck.IsClicklable = false;
            ProcedureTitleText.SetText(procedureDeck.procedureName);
            QuestionText.SetText(question.questionText);

            CardFront.SetActive(true);
            CardBack.SetActive(false);

            for (int i = 0; i < question.options.Length; i++)
            {
                string answer = question.options[i].answerText;
                if (question.options[i].isCorrect)
                    answer += " [C]";

                AnswerButtons[i].Setup(answer, i);
                AnswerOption answerOption = question.options[i];
                AnswerButtons[i].Button.onClick.AddListener(delegate { OnClickAnswerButton(answerOption); }); 
            }

            CardFront.transform.localScale = Vector3.zero;

            CardBack.transform.localScale = new Vector3(0f, 1f, 1f);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(CardFront.transform.DOScale(endValue: 1f, duration: 0.2f));
            sequence.Append(CardFront.transform.DOShakeScale(duration: 0.15f, strength: 0.2f));
            sequence.Join(CardFront.transform.DOShakeRotation(duration: 0.15f, strength: 0.2f));
            sequence.AppendInterval(1f);
            sequence.Append(CardFront.transform.DOScaleX(endValue: 0f, duration: 0.1f));
            sequence.AppendCallback(() =>
            {
                CardFront.SetActive(false);
                CardBack.SetActive(true);
            });
            sequence.Append(CardBack.transform.DOScaleX(endValue: 1f, duration: 0.1f));
            sequence.Append(CardBack.transform.DOShakeScale(duration: 0.15f, strength: 0.2f));
            sequence.Join(CardBack.transform.DOShakeRotation(duration: 0.15f, strength: 0.2f));
            QuizCardPanel.SetActive(true);
        }

        private void OnClickAnswerButton(AnswerOption option)
        {
            if(option.isCorrect)
            {
                //Add score
                QuizManager.Instance.AddScoreToCurrentUniversity();
                ShowResultPopUp(success: true);
            }
            else
            {
                //Nothing
                ShowResultPopUp(success: false);
            }
        }

        private IEnumerator EnableClickCoroutine()
        {
            yield return new WaitForSeconds(0.03f);
            Deck.IsClicklable = true;
        }

        private void ShowResultPopUp(bool success)
        {
            ResultPopUp.SetActive(true);
            if (success)
            {
                ResultText.SetText("Correto!");
                Instantiate(SuccessVFX, new Vector3(0f,0f,5f), Quaternion.identity);
            }
            else
            {
                ResultText.SetText("Errado!");
            }
        }

        public void DisableResultPopUp()
        {
            ResultPopUp.SetActive(false);
        }
    }
}
