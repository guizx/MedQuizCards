using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class Deck : MonoBehaviour
    {
        public ProcedureDeckData ProcedureDeck;

        public List<QuizQuestionData> Questions = new List<QuizQuestionData>();

        private QuestionUI questionUI;

        [SerializeField] private TextMeshProUGUI procedureTitleText;
        [SerializeField] private Button procedureButton;

        public static bool IsClicklable = true;

        public void Start()
        {
            IsClicklable = true;
            questionUI = FindObjectOfType<QuestionUI>();
            Questions.Clear();
            Questions.AddRange(ProcedureDeck.questions);
            Questions = ShuffleList(Questions);

            procedureTitleText.SetText(ProcedureDeck.procedureName);

            transform.localEulerAngles = new Vector3(0, 0, Random.Range(-10f, 10f));
        }

        private List<T> ShuffleList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int rnd = Random.Range(i, list.Count);
                (list[i], list[rnd]) = (list[rnd], list[i]);
            }

            return list;
        }

        public QuizQuestionData GetQuestion()
        {
            return Questions[Random.Range(0, Questions.Count)];
        }

        private void OnMouseDown()
        {
            if (!IsClicklable)
                return;

            questionUI.Setup(ProcedureDeck, GetQuestion());
        }
    }
}
