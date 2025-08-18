using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class Deck : MonoBehaviour
    {
        private QuizConfigData config;
        public ProcedureDeckData ProcedureDeck;
        public List<QuizQuestionData> Questions = new List<QuizQuestionData>();
        public static bool IsClicklable = true;

        public void Start()
        {
            IsClicklable = true;
            config = Resources.Load<QuizConfigData>("ScriptableObjects/QuizConfig");

            Questions.Clear();
            Questions.AddRange(ProcedureDeck.questions);

            if(config.ShuffleQuestions)
                Questions = ShuffleList(Questions);
            if (config.ShuffleAnswers)
            {
                for(int i = 0; i < Questions.Count; i++)
                {
                    Questions[i].options = ShuffleList(Questions[i].options.ToList()).ToArray();
                }
            }

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
            if (config.GetRandomQuestion)
            {
                return Questions[Random.Range(0, Questions.Count)];
            }
            else 
            {
                QuizQuestionData firstQuestion = Questions.FirstOrDefault();
                Questions.Remove(firstQuestion);
                Questions.Add(firstQuestion);
                return firstQuestion;
            }
        }

        private void OnMouseDown()
        {
            if (!IsClicklable)
                return;

            QuizManager.Instance?.SetCurrentQuestion(ProcedureDeck, GetQuestion());
        }
    }
}
