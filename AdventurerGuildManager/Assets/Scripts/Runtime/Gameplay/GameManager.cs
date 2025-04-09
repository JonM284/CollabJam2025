using System;
using Cysharp.Threading.Tasks;
using MoreMountains.Feedbacks;
using Project.Scripts.Utils;
using Runtime.Characters;
using Runtime.GameControllers;
using Runtime.UI.DataModels;
using UnityEngine;

namespace Runtime.Gameplay
{
    public class GameManager: MonoBehaviour
    {

        #region Instance

        public static GameManager Instance { get; private set; }

        #endregion
        
        #region Serialized Fields

        [SerializeField] private MMF_Player m_walkFeedback, m_giveQuestFeedback;
        [SerializeField] private CharacterBase m_character;

        #endregion

        #region GameControllerBase Inherited Methods

        private void OnEnable()
        {
            InteractionGameManager.onStartNextInteraction += InteractionGameManagerOnStartNextInteraction;
            DialogueDataModel.onDialogueClosed += DialogueDataModelOnDialogueClosed;
        }

        private void OnDisable()
        {
            InteractionGameManager.onStartNextInteraction -= InteractionGameManagerOnStartNextInteraction;
            DialogueDataModel.onDialogueClosed -= DialogueDataModelOnDialogueClosed;
        }

        public void Start()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
        }

        #endregion

        #region Class Implementation

        public void OnNewCharacterRequested()
        {
            InteractionGameManager.Instance.SetInteractionState(true);
            InteractionGameManager.Instance.MoveNextInteractionIndex();
            InteractionGameManager.Instance.GetCurrentInteraction();
        }

        private void DialogueDataModelOnDialogueClosed()
        {
            m_giveQuestFeedback.PlayFeedbacks();
        }
        
        private void InteractionGameManagerOnStartNextInteraction(KwestCharacterInfo _newCharacter)
        {
            ShowNextCharacter(_newCharacter);
        }
        
        public async UniTask ShowNextCharacter(KwestCharacterInfo _newCharacter)
        {
            //Check daily interaction list
            //If random interaction -> check adventurer or quest giver, and generate new character
            //If scripted -> do script
            
            m_character.AssignInfo(_newCharacter);
            
            //ToDo: wait for character to be created
            
            //Do walk animation
            m_walkFeedback.PlayFeedbacks();

            await UniTask.WaitUntil(() => !m_walkFeedback.IsPlaying);
            
            m_character.BeginInteraction();
        }
        
        #endregion
        
        
        
    }
}