using System.Collections.Generic;
using Runtime.Characters;
using Runtime.Quests;
using UnityEngine;

namespace Data.DataSaving
{
    
    [System.Serializable]
    public class SavedGameData
    {

        public List<Adventurer> activeAdventurers = new List<Adventurer>();
        public List<Adventurer> savedAdventurers = new List<Adventurer>();
        public List<QuestGiver> savedQuestGivers = new List<QuestGiver>();
        public List<Quest> activeQuests = new List<Quest>();
        public List<Quest> inactiveQuests = new List<Quest>();
        
        public SavedGameData()
        {
            activeAdventurers = new List<Adventurer>();
            savedAdventurers = new List<Adventurer>();
            savedQuestGivers = new List<QuestGiver>();
            activeQuests = new List<Quest>();
            inactiveQuests = new List<Quest>();
        }
    }
}