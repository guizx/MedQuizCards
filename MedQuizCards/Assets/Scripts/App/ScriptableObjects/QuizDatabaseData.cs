using UnityEngine;

namespace MedQuizCards
{
    [CreateAssetMenu(fileName = "QuizDatabase", menuName = "ScriptableObjects/Database")]
    public class QuizDatabase : ScriptableObject
    {
        public ProcedureDeckData[] decks;
    }
}
