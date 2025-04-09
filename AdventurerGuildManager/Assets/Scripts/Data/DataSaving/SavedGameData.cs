using System.Collections.Generic;
using Runtime.Characters;
using Runtime.Quests;
using UnityEngine;

namespace Data.DataSaving
{
    
    [System.Serializable]
    public class SavedGameData
    {

        public List<KwestCharacterInfo> activeAdventurers = new List<KwestCharacterInfo>();
        public List<KwestCharacterInfo> savedAdventurers = new List<KwestCharacterInfo>();
        public List<KwestCharacterInfo> savedQuestGivers = new List<KwestCharacterInfo>();
        public List<AssignableQuest> activeQuests = new List<AssignableQuest>();
        public List<AssignableQuest> inactiveQuests = new List<AssignableQuest>();
        
        public SavedGameData()
        {
            activeAdventurers = new List<KwestCharacterInfo>();
            savedAdventurers = new List<KwestCharacterInfo>();
            savedQuestGivers = new List<KwestCharacterInfo>();
            activeQuests = new List<AssignableQuest>();
            inactiveQuests = new List<AssignableQuest>();
        }
    }
}