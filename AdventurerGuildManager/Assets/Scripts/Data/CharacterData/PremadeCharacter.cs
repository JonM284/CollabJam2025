using System;
using Runtime.Characters;
using Runtime.Quests;
using UnityEngine;

namespace Data.CharacterData
{
    [Serializable]
    [CreateAssetMenu(menuName = "Kwest/ Character")]
    public class PremadeCharacter: ScriptableObject
    {
        public KwestCharacterInfo characterInfo;
        public Quest quest;
    }
}