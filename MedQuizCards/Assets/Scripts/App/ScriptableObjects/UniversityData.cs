using UnityEngine;

namespace MedQuizCards
{
    [CreateAssetMenu(fileName = "New University", menuName = "ScriptableObjects/University")]
    public class UniversityData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public Sprite Background;
        public Color Color;
    }
}
