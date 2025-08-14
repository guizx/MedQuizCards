using UnityEngine;

namespace MedQuizCards
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewProcedureDeck", menuName = "ScriptableObjects/Procedure Deck")]
    public class ProcedureDeckData : ScriptableObject
    {
        public string procedureName; 
        public Sprite procedureImage; 
        public QuizQuestionData[] questions = new QuizQuestionData[8];
    }
}
