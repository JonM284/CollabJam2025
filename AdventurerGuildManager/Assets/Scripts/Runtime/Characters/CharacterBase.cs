using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.CharacterData;
using MoreMountains.Feedbacks;
using Project.Scripts.Utils;
using Runtime.GameControllers;
using Runtime.UI.DataModels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Characters
{
    public class CharacterBase: MonoBehaviour, ICharacter
    {

        #region Serialized Fields

        [SerializeField] private MMF_Player m_talkingFeedback;

        [Header("Character Appearance")]
        [SerializeField] private List<SpriteRenderer> m_earRenderers = new List<SpriteRenderer>();
        [SerializeField] private List<SpriteRenderer> m_eyeRenderers = new List<SpriteRenderer>();
        [SerializeField] private List<SpriteRenderer> m_eyeColorRenderers = new List<SpriteRenderer>();
        [SerializeField] private List<SpriteRenderer> m_eyeBrowRenderers = new List<SpriteRenderer>();
        [SerializeField] private SpriteRenderer m_headRenderer;
        [SerializeField] private SpriteRenderer m_mouthRenderer;
        [SerializeField] private SpriteRenderer m_hairRenderer;
        [SerializeField] private SpriteRenderer m_bodyRenderer;
        [SerializeField] private SpriteRenderer m_leftArmRenderer, m_rightArmRenderer;
        [SerializeField] private SpriteRenderer m_leftWeaponRenderer, m_rightWeaponRenderer, m_backWeaponRenderer;
        
        #endregion
        
        #region Private Fields

        private Sprite m_savedEarSprite, m_savedEyeSprite, m_savedEyeColorSprite, m_savedEyeBrowSprite,
            m_savedLeftArmSprite, m_savedRightArmSprite;
        private Color m_savedFurColor;
        
        private PersonalityType m_checkPersonalityType;

        #endregion
        
        #region Accessors

        public bool m_isInteracting { get; protected set; }

        public KwestCharacterInfo assignedInfo { get; protected set; }

        public PersonalityType assignedPersonalityType { get; protected set; }
        

        #endregion

        #region Unity Events

        private void OnEnable()
        {
            DialogueDataModel.onLetterAdded += OnLetterAdded;
            InteractionGameManager.onFinishInteraction += FinishInteraction;
        }

        private void OnDisable()
        {
            DialogueDataModel.onLetterAdded -= OnLetterAdded;
            InteractionGameManager.onFinishInteraction -= FinishInteraction;
        }

        #endregion
        
        #region Class Implementation
        public void AssignInfo(KwestCharacterInfo _newInfo)
        {
            assignedInfo = _newInfo;
            
            m_checkPersonalityType  = CharacterGameController.Instance.GetPersonalityTypeByGUID(assignedInfo.personalityTypeGUID);
            
            if (m_checkPersonalityType.IsNull())
            {
                return;
            }

            assignedPersonalityType = m_checkPersonalityType;
        }

        public async UniTask ChangeCosmetics()
        {
            m_savedFurColor = CharacterGameController.Instance.GetFurColor(assignedInfo.furColorCosmeticIndex);
            m_savedEarSprite = CharacterGameController.Instance.GetEarSprite(assignedInfo.animalTypeIndex ,assignedInfo.earCosmeticIndex);
            m_savedEyeSprite = CharacterGameController.Instance.GetEyeSprite(assignedInfo.eyeCosmeticIndex);
            m_savedEyeColorSprite = CharacterGameController.Instance.GetEyeColor(assignedInfo.eyeColorCosmeticIndex);
            m_savedEyeBrowSprite = CharacterGameController.Instance.GetEyeBrowSprite(assignedInfo.eyeBrowCosmeticIndex);
            m_savedLeftArmSprite = CharacterGameController.Instance.GetArmSprite(assignedInfo.armTypeIndex, assignedInfo.leftArmCosmeticIndex);
            m_savedRightArmSprite = CharacterGameController.Instance.GetArmSprite(assignedInfo.armTypeIndex, assignedInfo.rightArmCosmeticIndex);
            
            m_earRenderers.ForEach(sr =>
            {
                sr.sprite = m_savedEarSprite;
                sr.color = m_savedFurColor;
            }); 
            
            m_eyeRenderers.ForEach(sr => sr.sprite = m_savedEyeSprite);
            m_eyeColorRenderers.ForEach(sr => sr.sprite = m_savedEyeColorSprite);
            m_eyeBrowRenderers.ForEach(sr => sr.sprite = m_savedEyeBrowSprite);
            
            m_headRenderer.sprite = CharacterGameController.Instance.GetHeadSprite(assignedInfo.headCosmeticIndex);
            m_headRenderer.color = m_savedFurColor;
            
            m_hairRenderer.sprite = CharacterGameController.Instance.GetHairSprite(assignedInfo.animalTypeIndex, assignedInfo.hairCosmeticIndex);
            m_hairRenderer.color = m_savedFurColor;
            
            m_mouthRenderer.sprite = CharacterGameController.Instance.GetMouthSprite(assignedInfo.animalTypeIndex, assignedInfo.mouthCosmeticIndex);
            m_bodyRenderer.sprite = CharacterGameController.Instance.GetBodySprite(assignedInfo.bodyCosmeticIndex);

            m_leftArmRenderer.sprite = m_savedLeftArmSprite;
            m_rightArmRenderer.sprite = m_savedRightArmSprite;

            m_leftWeaponRenderer.sprite = assignedInfo.leftWeaponIndex != -1 ?
                CharacterGameController.Instance.GetWeaponSprite(assignedInfo.leftWeaponIndex) : null;

            m_rightWeaponRenderer.sprite = assignedInfo.rightWeaponIndex != -1 ?
                CharacterGameController.Instance.GetWeaponSprite(assignedInfo.rightWeaponIndex) : null;

            m_backWeaponRenderer.sprite = assignedInfo.backWeaponIndex != -1 ?
                CharacterGameController.Instance.GetBackWeaponSprite(assignedInfo.backWeaponIndex) : null;
            
            await UniTask.Yield();
        }

        private void OnLetterAdded()
        {
            if (m_talkingFeedback.IsPlaying)
            {
                return;
            }
            
            m_talkingFeedback.PlayFeedbacks();
        }

        #endregion

        #region ICharacter Inherited Events

        public void BeginInteraction()
        {
            Debug.Log("START INTERACTION");

            m_isInteracting = true;
            
            if (assignedInfo.characterDialog.Count > 0)
            {
                DialogueGameController.Instance.DisplayNewSentences(assignedInfo.characterDialog);
            }
            else
            {
                DialogueGameController.Instance.DisplaySingleSentence(assignedPersonalityType.possibleDialogs[Random.Range(0, assignedPersonalityType.possibleDialogs.Count)]);
            }
        }

        public void FinishInteraction(bool _wasAccepted)
        {

            m_isInteracting = false;
            
            if (_wasAccepted)
            {
                if (assignedInfo.characterAcceptedDialog.Count > 0)
                {
                    DialogueGameController.Instance.DisplayNewSentences(assignedInfo.characterDialog);
                }
                else
                {
                    DialogueGameController.Instance.DisplaySingleSentence(assignedPersonalityType.possibleAcceptDialogs[Random.Range(0, assignedPersonalityType.possibleAcceptDialogs.Count)]);
                }
            }
            else
            {
                if (assignedInfo.characterDeniedDialog.Count > 0)
                {
                    DialogueGameController.Instance.DisplayNewSentences(assignedInfo.characterDeniedDialog);
                }
                else
                {
                    DialogueGameController.Instance.DisplaySingleSentence(assignedPersonalityType.possibleDenyDialogs[Random.Range(0, assignedPersonalityType.possibleDenyDialogs.Count)]);
                }
            }
            
            
        }
        

        #endregion


    }
}