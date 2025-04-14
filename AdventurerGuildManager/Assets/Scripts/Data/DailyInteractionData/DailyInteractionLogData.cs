using System;
using System.Collections.Generic;
using Data.CharacterData;
using NUnit.Framework;
using UnityEngine;

namespace Data.DailyInteractionData
{
    [CreateAssetMenu(menuName = "Kwest/ Daily Interaction List")]
    public class DailyInteractionLogData: ScriptableObject
    {
        
        #region Nested Classes

        [Serializable]
        public class InteractableCharacters
        {
            public CharacterType characterType;
            public PremadeCharacter premadeCharacter;
        }
        
        public enum CharacterType
        {
            RANDOM_ADVENTURER,
            SCRIPTED,
            RANDOM_QUEST_GIVER,
        }

        #endregion


        #region Class Implementation

        [Tooltip("If this is set to TRUE, the assigned characters will randomly appear at some point in the day," +
                 "between random characters")]
        public bool isRandomTiming;
        
        public int setAmountOfCharactersInDay = 0;
        
        public List<InteractableCharacters> dailyCharacters = new List<InteractableCharacters>();

        #endregion


    }
}