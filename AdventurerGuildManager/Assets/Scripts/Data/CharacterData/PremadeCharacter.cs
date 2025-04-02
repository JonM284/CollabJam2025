using UnityEngine;

namespace Data.CharacterData
{
    
    [CreateAssetMenu(menuName = "Kwest/ Character")]
    public class PremadeCharacter: ScriptableObject
    {
        public string characterName;
        
        public int characterStr;
        public int characterInt;
        public int characterCha;
    }
}