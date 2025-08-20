using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace MedQuizCards
{
    public class RankingLeaderboard : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI RankingPositionText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI UniversityNameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }

        public UniversityRanking UniversityRanking { get; private set; }

        public void Setup(int position, UniversityRanking university)
        {
            UniversityRanking = university;
            RankingPositionText.SetText($"{position}.");
            UniversityNameText.SetText($"{university.UniversityName}");
            ScoreText.SetText($"{university.Score.ToString("D2")}");
        }

        public void UpdateUI(int position)
        {
            if (UniversityRanking == null)
                return;

            RankingPositionText.SetText($"{position}.");
            UniversityNameText.SetText($"{UniversityRanking.UniversityName}");
            ScoreText.SetText($"{UniversityRanking.Score.ToString("D2")}");
        }
    }
}
