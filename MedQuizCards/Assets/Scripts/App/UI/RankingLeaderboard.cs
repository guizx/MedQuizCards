using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class RankingLeaderboard : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI RankingPositionText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI UniversityNameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }

        public UniversityRanking UniversityRanking { get; private set; }

        [SerializeField] private Sprite[] medalSprites;

        [SerializeField] private GameObject medalContainer;
        [field: SerializeField] public Image MedalImage { get; private set; }

        [field: SerializeField] public TextMeshProUGUI MedalRankingPositionText { get; private set; }

        public void Setup(int position, UniversityRanking university)
        {
            if(position <= 3 && university.Pontos > 0)
            {
                medalContainer.SetActive(true);
                int indexMedal = position - 1;
                MedalImage.sprite = medalSprites[indexMedal];
                MedalRankingPositionText.SetText($"{position}");
                RankingPositionText.gameObject.SetActive(false);
            }
            else
            {
                medalContainer.SetActive(false);
                RankingPositionText.gameObject.SetActive(true);
            }

            UniversityRanking = university;
            RankingPositionText.SetText($"{position}.");
            string name = $"{university.IES} {university.Campus}";
            UniversityNameText.SetText(name);
            if (QuizManager.Instance.ShowInUppercase)
                UniversityNameText.fontStyle = FontStyles.UpperCase;
            ScoreText.SetText($"{UniversityRanking.Pontos.ToString("D2")} pts");
        }

        public void UpdateUI(int position)
        {
            if (UniversityRanking == null)
                return;

            if (position <= 3 && UniversityRanking.Pontos > 0)
            {
                medalContainer.SetActive(true);
                int indexMedal = position - 1;
                MedalImage.sprite = medalSprites[indexMedal];
                MedalRankingPositionText.SetText($"{position}");
                RankingPositionText.gameObject.SetActive(false);
            }
            else
            {
                medalContainer.SetActive(false);
                RankingPositionText.gameObject.SetActive(true);
            }

            RankingPositionText.SetText($"{position}.");
            string name = $"{UniversityRanking.IES} {UniversityRanking.Municipio}";
            UniversityNameText.SetText(name);
            if (QuizManager.Instance.ShowInUppercase)
                UniversityNameText.fontStyle = FontStyles.UpperCase;

            ScoreText.SetText($"{UniversityRanking.Pontos.ToString("D2")} pts");
        }

    }
}
