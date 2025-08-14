using UnityEngine;

namespace MedQuizCards
{
    [System.Serializable]
    public class AnswerOption
    {
        [TextArea] public string answerText; 
        public bool isCorrect;               
    }

    [CreateAssetMenu(fileName = "NewQuestion", menuName = "ScriptableObjects/Question")]
    public class QuizQuestionData : ScriptableObject
    {
        [TextArea] public string questionText; 
        public AnswerOption[] options = new AnswerOption[4];
    }
}
