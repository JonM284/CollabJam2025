using System.Collections.Generic;
using Data.DataSaving;
using Runtime.Characters;
using Runtime.Quests;

namespace Runtime.GameControllers
{
    public class QuestController: GameControllerBase, ISaveableData
    {
        
        #region Instance

        public static QuestController Instance { get; private set; }

        #endregion

        #region Private Fields

        private List<Quest> m_activeQuests = new List<Quest>();

        private List<Quest> m_savedQuests = new List<Quest>();

        #endregion
        
        #region Saved Game Data

        public void LoadData(SavedGameData _savedGameData)
        {
            m_activeQuests = _savedGameData.activeQuests;
            m_savedQuests = _savedGameData.inactiveQuests;
        }

        public void SaveData(ref SavedGameData _savedGameData)
        {
            _savedGameData.activeQuests = m_activeQuests;
            _savedGameData.inactiveQuests = m_savedQuests;
        }

        #endregion
        
        
    }
}