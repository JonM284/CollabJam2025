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
        [SerializeField] private CosmeticData m_cosmeticData;

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

        public void AssignRandomCosmetics(KwestCharacterInfo _character)
        {
            if (_character.IsNull())
            {
                return;
            }

            _character.animalTypeIndex = Random.Range(0, m_cosmeticData.animalSpecificCosmetics.Count);
            _character.furColorCosmeticIndex = Random.Range(0, m_cosmeticData.furColors.Count);
            _character.headCosmeticIndex = Random.Range(0, m_cosmeticData.headCosmetics.Count); 
            
            _character.earCosmeticIndex = Random.Range(0,
                m_cosmeticData.animalSpecificCosmetics[_character.animalTypeIndex].earCosmetic.Count);
            _character.hairCosmeticIndex = Random.Range(0,
                m_cosmeticData.animalSpecificCosmetics[_character.animalTypeIndex].hairCosmetic.Count);
            _character.mouthCosmeticIndex = Random.Range(0,
                m_cosmeticData.animalSpecificCosmetics[_character.animalTypeIndex].mouthCosmetic.Count);
            
            _character.eyeCosmeticIndex = Random.Range(0, m_cosmeticData.eyeCosmetics.Count);
            _character.eyeColorCosmeticIndex = Random.Range(0, m_cosmeticData.eyeColorCosmetics.Count);
            _character.eyeBrowCosmeticIndex = Random.Range(0, m_cosmeticData.eyeBrowCosmetics.Count);
            _character.bodyCosmeticIndex = Random.Range(0, m_cosmeticData.bodyCosmetics.Count);
            _character.armTypeIndex = Random.Range(0, m_cosmeticData.armCoupleData.Count);
            _character.leftArmCosmeticIndex = Random.Range(0, 2);
            _character.rightArmCosmeticIndex = Random.Range(0, 2);

            _character.leftWeaponIndex = _character.leftArmCosmeticIndex == 0 
                ? Random.Range(0, m_cosmeticData.holdableWeapons.Count) : _character.leftWeaponIndex = -1;

            _character.rightWeaponIndex = _character.rightArmCosmeticIndex == 0 
                ? Random.Range(0, m_cosmeticData.holdableWeapons.Count) : _character.rightWeaponIndex = -1;

            _character.backWeaponIndex = Random.Range(0,2) == 0 
                ? Random.Range(0, m_cosmeticData.backWeapons.Count) : -1;
        }

        public void SaveAdventurer(KwestCharacterInfo _character)
        {
            if (_character.IsNull())
            {
                return;
            }

            m_savedAdventurers.Add(_character);
        }

        public void AssignCharacterActiveQuest(KwestCharacterInfo _character)
        {
            if (_character.IsNull())
            {
                return;
            }

            if (m_savedAdventurers.Contains(_character))
            {
                m_savedAdventurers.Remove(_character);
            }

            m_activeAdventurers.Add(_character);
        }

        #region Cosmetics

        public Color GetFurColor(int _index)
        {
            return _index >= m_cosmeticData.furColors.Count
                ? m_cosmeticData.furColors.FirstOrDefault() 
                : m_cosmeticData.furColors[_index];
        }

        public Sprite GetHeadSprite(int _index)
        {
            return _index >= m_cosmeticData.headCosmetics.Count
                ? m_cosmeticData.headCosmetics.FirstOrDefault()
                : m_cosmeticData.headCosmetics[_index];
        }
        
        public Sprite GetEarSprite(int _animalType, int _index)
        {
            return _index >= m_cosmeticData.animalSpecificCosmetics[_animalType].earCosmetic.Count
                ? m_cosmeticData.animalSpecificCosmetics[_animalType].earCosmetic.FirstOrDefault()
                : m_cosmeticData.animalSpecificCosmetics[_animalType].earCosmetic[_index];
        }
        
        public Sprite GetHairSprite(int _animalType, int _index)
        {
            return _index >= m_cosmeticData.animalSpecificCosmetics[_animalType].hairCosmetic.Count
                ? m_cosmeticData.animalSpecificCosmetics[_animalType].hairCosmetic.FirstOrDefault()
                : m_cosmeticData.animalSpecificCosmetics[_animalType].hairCosmetic[_index];
        }

        public Sprite GetEyeSprite(int _index)
        {
            return _index >= m_cosmeticData.eyeCosmetics.Count
                ? m_cosmeticData.eyeCosmetics.FirstOrDefault()
                : m_cosmeticData.eyeCosmetics[_index];
        }

        public Sprite GetEyeColor(int _index)
        {
            return _index >= m_cosmeticData.eyeCosmetics.Count
                ? m_cosmeticData.eyeColorCosmetics.FirstOrDefault()
                : m_cosmeticData.eyeColorCosmetics[_index];
        }
        
        public Sprite GetEyeBrowSprite(int _index)
        {
            return _index >= m_cosmeticData.eyeBrowCosmetics.Count
                ? m_cosmeticData.eyeBrowCosmetics.FirstOrDefault()
                : m_cosmeticData.eyeBrowCosmetics[_index];
        }
        
        public Sprite GetBodySprite(int _index)
        {
            return _index >= m_cosmeticData.bodyCosmetics.Count
                ? m_cosmeticData.bodyCosmetics.FirstOrDefault()
                : m_cosmeticData.bodyCosmetics[_index];
        }
        
        public Sprite GetArmSprite(int _armCoupleIndex, int _index)
        {
            return _index >= m_cosmeticData.armCoupleData[_armCoupleIndex].armCosmetics.Count
                ? m_cosmeticData.armCoupleData[_armCoupleIndex].armCosmetics.FirstOrDefault()
                : m_cosmeticData.armCoupleData[_armCoupleIndex].armCosmetics[_index];
        }
        
        public Sprite GetMouthSprite(int _animalType, int _index)
        {
            return _index >= m_cosmeticData.animalSpecificCosmetics[_animalType].mouthCosmetic.Count
                ? m_cosmeticData.animalSpecificCosmetics[_animalType].mouthCosmetic.FirstOrDefault()
                : m_cosmeticData.animalSpecificCosmetics[_animalType].mouthCosmetic[_index];
        }

        public Sprite GetWeaponSprite(int _index)
        {
            return _index >= m_cosmeticData.holdableWeapons.Count
                ? m_cosmeticData.holdableWeapons.FirstOrDefault()
                : m_cosmeticData.holdableWeapons[_index];
        }

        public Sprite GetBackWeaponSprite(int _index)
        {
            return _index >= m_cosmeticData.backWeapons.Count
                ? m_cosmeticData.backWeapons.FirstOrDefault()
                : m_cosmeticData.backWeapons[_index];
        }
        
        
        #endregion
        
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