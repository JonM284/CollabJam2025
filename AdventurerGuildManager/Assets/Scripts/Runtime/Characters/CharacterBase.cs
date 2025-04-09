using System;
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

        #endregion
        
        #region Private Fields

        private PersonalityType m_checkPersonalityType;

        #endregion
        
        #region Accessors

        public KwestCharacterInfo assignedInfo { get; protected set; }

        public PersonalityType assignedPersonalityType { get; protected set; }
        

        #endregion

        #region Unity Events

        private void OnEnable()
        {
            DialogueDataModel.onLetterAdded += OnLetterAdded;
        }

        private void OnDisable()
        {
            DialogueDataModel.onLetterAdded -= OnLetterAdded;
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
            
            if (assignedInfo.characterDialog.Count > 0)
            {
                DialogueGameController.Instance.DisplayNewSentences(assignedInfo.characterDialog);
            }
            else
            {
                DialogueGameController.Instance.DisplaySingleSentence(assignedPersonalityType.possibleDialogs[Random.Range(0, assignedPersonalityType.possibleDialogs.Count)]);
            }
        }

        public void OnDeny()
        {
            
        }

        public void OnAccept()
        {
            
        }
        

        #endregion


    }
}