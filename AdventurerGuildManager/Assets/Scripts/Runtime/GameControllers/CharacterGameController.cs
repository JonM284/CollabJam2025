using System.Collections.Generic;
using Data.DataSaving;
using Runtime.Characters;

namespace Runtime.GameControllers
{
    public class CharacterGameController: GameControllerBase, ISaveableData
    {
        
        #region Instance

        public static CharacterGameController Instance { get; private set; }

        #endregion

        #region Private Fields

        private List<Adventurer> m_activeAdventurers = new List<Adventurer>();
        private List<Adventurer> m_savedAdventurers = new List<Adventurer>();
        private List<QuestGiver> m_savedQuestGivers = new List<QuestGiver>();

        #endregion
        
        #region Saved Game Data

        public void LoadData(SavedGameData _savedGameData)
        {
            m_activeAdventurers = _savedGameData.activeAdventurers;
            m_savedAdventurers = _savedGameData.savedAdventurers;
            m_savedQuestGivers = _savedGameData.savedQuestGivers;
        }

        public void SaveData(ref SavedGameData _savedGameData)
        {
            _savedGameData.activeAdventurers = m_activeAdventurers;
            _savedGameData.savedAdventurers = m_savedAdventurers;
            _savedGameData.savedQuestGivers = m_savedQuestGivers;
        }

        #endregion
        
        
    }
}