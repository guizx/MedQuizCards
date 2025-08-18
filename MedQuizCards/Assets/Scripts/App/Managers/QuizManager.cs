using Nato.Singleton;
using Nato.StateMachine;
using System;
using UnityEngine;

namespace MedQuizCards
{
    public class QuizManager : Singleton<QuizManager>
    {
        [SerializeField] private GameObject decksParent;

        private ProcedureDeckData currentProcedureDeck;
        private QuizQuestionData currentQuestion;

        [field: SerializeField] public UniversityRanking CurrentUniversity { get; private set; }

        [field: SerializeField] public UniversityRanking[] UniversityRankings { get; private set; }

        public void SetCurrentQuestion(ProcedureDeckData deck, QuizQuestionData question)
        {
            currentProcedureDeck = deck;
            currentQuestion = question;

            UIQuestionState questionState = UIStates.Instance.QuestionState;
            UIStateManager.Instance.StateMachine.TransitionTo(questionState);
            questionState.Manager.UIPanels.QuestionUI?.Setup(deck, question);
        }

        public void DisableDecks()
        {
            decksParent.SetActive(false);
        }

        public void EnableDecks()
        {
            decksParent.SetActive(true);
        }

        public void SetCurrentUniversity(UniversityRanking universityRanking)
        {
            CurrentUniversity = universityRanking;
        }

        public void AddScoreToCurrentUniversity()
        {
            CurrentUniversity.Score++;
        }
    }

    [System.Serializable]
    public class UniversityRanking
    {
        public UniversityData UniversityData;
        public int Score;
    }
}
