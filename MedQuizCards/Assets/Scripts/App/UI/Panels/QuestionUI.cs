using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class QuestionUI : MonoBehaviour
    {
        public GameObject QuizCardPanel;
        public GameObject CardFront;
        public GameObject CardBack;

        public TextMeshProUGUI ProcedureTitleText;
        public TextMeshProUGUI QuestionText;

        public Button[] AnswerButtons;

        public void Setup(ProcedureDeckData procedureDeck, QuizQuestionData question)
        {
            Deck.IsClicklable = false;
            ProcedureTitleText.SetText(procedureDeck.procedureName);
            QuestionText.SetText(question.questionText);

            CardFront.SetActive(true);
            CardBack.SetActive(false);

            for (int i = 0; i < question.options.Length; i++)
            {
                AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(question.options[i].answerText);
                AnswerOption answerOption = question.options[i];
                AnswerButtons[i].onClick.AddListener(delegate { OnClickAnswerButton(answerOption); }); 
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
            }
            else
            {
                //Nothing
            }
            QuizCardPanel.SetActive(false);
            StartCoroutine(EnableClickCoroutine());
        }

        private IEnumerator EnableClickCoroutine()
        {
            yield return new WaitForSeconds(0.03f);
            Deck.IsClicklable = true;
        }
    }
}
