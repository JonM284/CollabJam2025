using System;
using Runtime.Quests;
using UnityEngine;

namespace Data.CharacterData
{
    [Serializable]
    [CreateAssetMenu(menuName = "Kwest/ Quest")]
    public class PremadeQuest: ScriptableObject
    {
        public Quest Quest;
    }
}