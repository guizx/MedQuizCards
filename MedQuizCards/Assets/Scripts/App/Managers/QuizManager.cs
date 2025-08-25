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

        public UniversityDatabase UniversityDatabase;

        public static Action OnDatabaseReload;


        private void Start()
        {
            OnDatabaseReload += HandleDatabaseReload;
            HandleDatabaseReload();
        }

        private void OnDestroy()
        {
            OnDatabaseReload -= HandleDatabaseReload;
        }

        private void HandleDatabaseReload()
        {
            UniversityRankings.Clear();
            for (int i = 0; i < UniversityDatabase.Universities.Count; i++)
            {
                UniversityDTO universityDTO = UniversityDatabase.Universities[i];

                UniversityRanking university = new UniversityRanking();
                university.ID = int.Parse(universityDTO.id);
                university.IES = universityDTO.ies;
                university.Municipio = universityDTO.municipio;
                university.UF = universityDTO.uf;
                university.Pontos = int.Parse(universityDTO.pontos);
                university.Campus = universityDTO.campus;


                if (RemoveAccents)
                {
                    university.IES = StringUtils.RemoveAccents(university.IES);
                    university.Campus = StringUtils.RemoveAccents(university.Campus);
                }

                university.CompleteName = $"{university.IES} {university.Campus}";
                UniversityRankings.Add(university);
            }


            UniversityRankings = UniversityRankings.OrderBy(n => n.IES).ToList();
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
            CurrentUniversity.Pontos++;

            UniversityDatabase.AddScoreAndSave(CurrentUniversity);

        }

        public void AddNewUniversity(UniversityDTO universityDTO)
        {
            //UniversityRanking university = new UniversityRanking();
            //university.ID = int.Parse(universityDTO.id);
            //university.IES = universityDTO.ies;
            //university.Campus = universityDTO.campus;
            //university.UF = universityDTO.uf;
            //university.Pontos = 0;
            
            //if (RemoveAccents)
            //{
            //    university.IES = StringUtils.RemoveAccents(university.IES);
            //    university.Campus = StringUtils.RemoveAccents(university.Campus);
            //}
            //university.CompleteName = $"{university.IES} {university.Campus}";
            //UniversityRankings.Add(university);
            UniversityDatabase.AddUniversity(universityDTO);
        }
    }

    [System.Serializable]
    public class UniversityRanking
    {
        public int ID;
        public string IES;
        public string UF;
        public string Municipio;
        public int Pontos;
        public string Campus;
        public string CompleteName;

    }

    [System.Serializable]
    public class UniversityDTO
    {
        public string id;
        public string uf;
        public string municipio;
        public string ies;
        public string campus;
        public string pontos;
    }
}
