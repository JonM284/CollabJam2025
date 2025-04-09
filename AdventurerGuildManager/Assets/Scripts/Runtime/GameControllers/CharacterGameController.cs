using System.Collections.Generic;
using System.Linq;
using Data.CharacterData;
using Data.DataSaving;
using Project.Scripts.Utils;
using Runtime.Characters;
using UnityEngine;

namespace Runtime.GameControllers
{
    public class CharacterGameController: GameControllerBase, ISaveableData
    {
        
        #region Instance

        public static CharacterGameController Instance { get; private set; }

        #endregion

        #region Serialized Fields

        [SerializeField] private List<PersonalityType> m_personalities = new List<PersonalityType>();

        #endregion

        #region Private Fields

        private List<KwestCharacterInfo> m_activeAdventurers = new List<KwestCharacterInfo>();
        private List<KwestCharacterInfo> m_savedAdventurers = new List<KwestCharacterInfo>();
        private List<KwestCharacterInfo> m_savedQuestGivers = new List<KwestCharacterInfo>();

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

        public PersonalityType GetPersonalityTypeByGUID(string _searchGUID)
        {
            return m_personalities.FirstOrDefault(pt => pt.GUID == _searchGUID);
        }
        
        public KwestCharacterInfo CreateRandomQuestGiver()
        {
            //ToDo: make actually good. Probably use weighted probability
            return new KwestCharacterInfo("Ran Dom", true ,true, m_personalities[Random.Range(0, m_personalities.Count)].GUID,
                0, 0, 0);
        }

        public KwestCharacterInfo CreateRandomAdventurer()
        {
            //ToDo: make actually good. Probably use weighted probability
            return new KwestCharacterInfo("Ran Dom", false ,true, m_personalities[Random.Range(0, m_personalities.Count)].GUID,
                Random.Range(0,5), Random.Range(0,5), Random.Range(0,5));
        }

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