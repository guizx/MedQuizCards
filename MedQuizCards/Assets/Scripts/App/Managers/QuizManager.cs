using Nato.Singleton;
using Nato.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MedQuizCards
{
    public class QuizManager : Singleton<QuizManager>
    {
        [SerializeField] private GameObject decksParent;

        private ProcedureDeckData currentProcedureDeck;
        private QuizQuestionData currentQuestion;

        [field: SerializeField] public UniversityRanking CurrentUniversity { get; private set; }

        [field: SerializeField] public List<UniversityRanking> UniversityRankings { get; private set; } = new List<UniversityRanking>();

        public List<string> UniversitiesNames = new List<string>();
        public List<string> UniversitiesCities = new List<string>();
        public List<string> UniversitiesUFs = new List<string>();
        public CSVReader CSVReader;

        public string CurrentUF;

        public bool ShowInUppercase;
        public bool RemoveAccents;


        protected override void Awake()
        {
            base.Awake();
            UniversitiesNames = CSVReader.GetColumn(4); // Nome
            UniversitiesCities = CSVReader.GetColumn(5); // Cidade
            UniversitiesUFs = CSVReader.GetColumn(0); // Estado
            for (int i = 0;  i < UniversitiesNames.Count; i++)
            {
                UniversityRanking university = new UniversityRanking();
                university.Index = i;

                if (RemoveAccents)
                {
                    university.UniversityName = StringUtils.RemoveAccents(UniversitiesNames[i]);
                    university.City = StringUtils.RemoveAccents(UniversitiesCities[i]);
                }
                else
                {
                    university.UniversityName = UniversitiesNames[i];
                    university.City = UniversitiesCities[i];
                }

                university.CompleteName = $"{university.UniversityName} {university.City}"; 
                university.UF = UniversitiesUFs[i];
                university.Score = 0;

                UniversityRankings.Add(university); 
            }

            UniversityRankings = UniversityRankings.OrderBy(n => n.UniversityName).ToList();
        }

        public List<UniversityRanking>  GetUniversitiesByUF(string uf)
        {
            string ufNormalized = uf.Trim().ToUpper();

            return UniversityRankings
                .Where(u => u.UF != null && u.UF.Trim().ToUpper() == ufNormalized)
                .ToList();
        }


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
        public int Index;
        public string UniversityName;
        public string City;
        public string UF;
        public int Score;
        public string CompleteName;

    }
}
