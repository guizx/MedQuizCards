using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MedQuizCards
{
    public class RankingLeaderboard : MonoBehaviour
    {
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }

        public void Setup(Sprite icon, int score)
        {
            Icon.sprite = icon;
            ScoreText.SetText(score.ToString("D2"));
        }

    }
}
