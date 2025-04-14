using System;
using Cysharp.Threading.Tasks;
using MoreMountains.Feedbacks;
using Project.Scripts.Utils;
using Runtime.Characters;
using Runtime.GameControllers;
using Runtime.ScriptedAnimations.Transform;
using Runtime.UI.DataModels;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Gameplay
{
    public class GameManager: MonoBehaviour
    {

        #region Instance

        public static GameManager Instance { get; private set; }

        #endregion
        
        #region Serialized Fields

        [SerializeField] private MMF_Player m_walkFeedback;
        [SerializeField] private MMF_Player m_giveObjectFeedback;
        [SerializeField] private TransformAnimation m_walkAnim;
        [SerializeField] private CharacterBase m_character;

        [SerializeField] private DraggableObject m_draggable;
        
        [SerializeField] private GameObject m_denyArea;
        
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
            if (m_character.m_isInteracting)
            {
                m_giveObjectFeedback.PlayFeedbacks();
                m_denyArea.SetActive(true);
            }
            else
            {
                CharacterEndInteraction();
                m_denyArea.SetActive(false);
            }
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
            await m_character.ChangeCosmetics();
            
            //Do walk animation
            m_walkFeedback.PlayFeedbacks();
            m_walkAnim.Play();

            await UniTask.WaitUntil(() => !m_walkAnim.isPlaying);
            
            m_character.BeginInteraction();
        }

        private async UniTask CharacterEndInteraction()
        {
            m_walkFeedback.PlayFeedbacks();
            m_walkAnim.PlayReverse();

            await UniTask.WaitUntil(() => !m_walkAnim.isPlaying);
            
            InteractionGameManager.Instance.SetInteractionState(false);
        }
        
        
        
        #endregion
        
        
        
    }
}