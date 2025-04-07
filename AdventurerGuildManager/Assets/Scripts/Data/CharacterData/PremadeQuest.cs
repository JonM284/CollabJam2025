using Runtime.Quests;
using UnityEngine;

namespace Data.CharacterData
{
    [CreateAssetMenu(menuName = "Kwest/ Quest")]
    public class PremadeQuest: ScriptableObject
    {
        public Quest Quest;
    }
}