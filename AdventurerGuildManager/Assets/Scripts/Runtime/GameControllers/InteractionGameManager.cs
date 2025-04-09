using System;
using System.Collections.Generic;
using Data.CharacterData;
using Data.DailyInteractionData;
using NUnit.Framework;
using Project.Scripts.Utils;
using Runtime.Characters;
using Runtime.Gameplay;
using Runtime.Quests;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.GameControllers
{
    public class InteractionGameManager: GameControllerBase
    {
        
        #region Instance

        public static InteractionGameManager Instance { get; private set; }

        #endregion

        #region Actions

        public static event Action onDailyLimitReached;

        public static event Action<KwestCharacterInfo> onStartNextInteraction; 

        #endregion

        #region Serialized Fields

        [SerializeField]
        private List<DailyInteractionLogData> m_dailyInteractions = new List<DailyInteractionLogData>();

        #endregion

        #region Private Fields

        private int m_currentDay = 0, m_currentInteractionIndex = -1;
        private DailyInteractionLogData m_currentDailyInteractions;
        private DailyInteractionLogData.InteractableCharacters m_currentInteraction;

        #endregion

        #region Accessors

        public DraggableObject currentDragable { get; private set; }
        
        public Hoverable currentHoverable { get; private set; }

        public bool isHoldingObject => !currentDragable.IsNull();

        public bool isCurrentlyInteracting { get; private set; }

        #endregion

        #region Unity Events

        /*private void Update()
        {
            CheckDraggingObjectSize();
        }*/

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

        private void CheckDraggingObjectSize()
        {
            if (!isHoldingObject || !currentDragable.canBePlaced)
            {
                return;
            }

            if (currentHoverable.IsNull())
            {
                return;
            }

            if (currentDragable.placementLoc != currentHoverable.GetHoverableType())
            {
                return;
            }

            if (currentDragable.isSmall)
            {
                return;
            }

            Debug.Log("Shrink");
            currentDragable.ChangeSize(true);
        }

        public void MoveNextInteractionIndex() => m_currentInteractionIndex++;

        public void GetCurrentInteraction()
        {
            if (m_currentDailyInteractions.IsNull())
            {
                m_currentDailyInteractions = m_dailyInteractions[m_currentDay];
            }
            
            if (m_currentInteractionIndex >= m_currentDailyInteractions.dailyCharacters.Count)
            {
                //END DAY
                onDailyLimitReached?.Invoke();
                return ;
            }

            m_currentInteraction = m_currentDailyInteractions.dailyCharacters[m_currentInteractionIndex];
            
            if (m_currentInteraction.characterType != DailyInteractionLogData.CharacterType.SCRIPTED)
            {
                //Make random character
                KwestCharacterInfo _kwestCharacterInfo;
                switch (m_currentInteraction.characterType)
                {
                    case DailyInteractionLogData.CharacterType.RANDOM_QUEST_GIVER:
                        //ToDo: change
                        _kwestCharacterInfo = CharacterGameController.Instance.CreateRandomQuestGiver();
                        break;
                    
                    case DailyInteractionLogData.CharacterType.RANDOM_ADVENTURER:
                        _kwestCharacterInfo = CharacterGameController.Instance.CreateRandomAdventurer();
                        break;
                    
                    default:
                        _kwestCharacterInfo = Random.Range(0,2) == 0 ? CharacterGameController.Instance.CreateRandomAdventurer() 
                            : CharacterGameController.Instance.CreateRandomQuestGiver();
                        break;
                }
                
                onStartNextInteraction?.Invoke(_kwestCharacterInfo);
                return;
            }
            
            onStartNextInteraction?.Invoke(m_currentDailyInteractions.dailyCharacters[m_currentInteractionIndex].premadeCharacter.characterInfo);
        }

        public void ResetInteractionIndex() => m_currentInteractionIndex = 0;

        public void MoveNextDay()
        {
            m_currentDay++;

            if (m_currentDay >= m_dailyInteractions.Count)
            {
                Debug.LogError("Current Day is HIGHER than amount of days planned");
                return;
            }
            
            m_currentDailyInteractions = m_dailyInteractions[m_currentDay];
        }

        public DailyInteractionLogData GetInteractionLog() => m_currentDailyInteractions;

        public int GetDay() => m_currentDay;

        public void SetInteractionState(bool _isInteracting)
        {
            isCurrentlyInteracting = _isInteracting;
        }

        public void SetCurrentDraggable(DraggableObject _draggableObject)
        {
            if (_draggableObject.IsNull())
            {
                return;
            }

            currentDragable = _draggableObject;
        }

        public void OnDraggableReleased()
        {
            
        }

        private void ResetCurrentDraggable()
        {
            currentDragable = null;
        }

        private void CheckDraggablePlacement()
        {
            if (currentHoverable.IsNull())
            {
                return;
            }

            if (currentHoverable.GetHoverableType() != currentDragable.placementLoc)
            {
                return;
            }

            switch (currentDragable.placementLoc)
            {
                case HoverableTypes.QUEST_BOARD:
                    SaveNewQuest();
                    break;
                case HoverableTypes.ADVENTURER_BOOK:
                    //Add New Adventurer
                    SaveNewAdventurer();
                    break;
                default:
                    
                    break;
            }
        }

        private void SaveNewQuest()
        {
            //QuestController.Instance.AddQuestToQuestBoard(GetCurrentQuest);
        }

        private Quest GetCurrentQuest()
        {
            return null;
        }
        
        private void SaveNewAdventurer()
        {
            
        }
        
        public void SetCurrentHovered(Hoverable _hoverable)
        {
            if (_hoverable.IsNull())
            {
                return;
            }

            currentHoverable = _hoverable;

            if (currentHoverable.IsNull() || currentDragable.IsNull())
            {
                return;
            }

            if (currentHoverable.GetHoverableType() != currentDragable.placementLoc)
            {
                return;
            }

            currentDragable.ChangeSize(true);
        }

        public void ResetCurrentHovered()
        {
            currentHoverable = null;

            if (currentDragable.IsNull())
            {
                return;
            }

            if (!currentDragable.isSmall)
            {
                return;
            }

            currentDragable.ChangeSize(false);
        }
        
        #endregion
        
    }
}