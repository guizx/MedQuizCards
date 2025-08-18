using UnityEngine;

namespace MedQuizCards
{
    [CreateAssetMenu(fileName = "NewQuizConfig", menuName = "ScriptableObjects/Quiz Config")]
    public class QuizConfigData : ScriptableObject
    {
        public bool ShuffleQuestions = true;
        public bool ShuffleAnswers = false;
        public bool GetRandomQuestion = true;
    }
}
