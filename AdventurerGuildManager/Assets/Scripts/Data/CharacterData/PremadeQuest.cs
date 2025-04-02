using UnityEngine;

namespace Data.CharacterData
{
    [CreateAssetMenu(menuName = "Kwest/ Quest")]
    public class PremadeQuest: ScriptableObject
    {
        public int strRequirement;
        public int intRequirement;
        public int chaRequirement;
        
        public string title;
        public string description;

        public int reward;
        
        public int difficultyRating;
    }
}