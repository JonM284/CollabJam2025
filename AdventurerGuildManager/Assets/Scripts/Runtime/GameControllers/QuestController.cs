using System.Collections.Generic;
using Data.DataSaving;
using Project.Scripts.Utils;
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

        private List<AssignableQuest> m_activeQuests = new List<AssignableQuest>();
        private List<AssignableQuest> m_savedQuests = new List<AssignableQuest>();

        #endregion

        #region GameControllerBase Inherited Methods

        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
            base.Initialize();
        }

        #endregion

        #region Class Implementation

        public void AddQuestToQuestBoard(Quest _quest)
        {
            m_savedQuests.Add(new AssignableQuest(_quest));
        }

        

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