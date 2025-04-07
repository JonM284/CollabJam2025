using System;
using System.Collections.Generic;
using Runtime.Characters;

namespace Runtime.Quests
{
    [Serializable]
    public class Quest
    {
        public int strengthRequirement;
        public int intelligenceRequirement;
        public int charismaRequirement;
        
        public string title;
        public string description;

        public int reward;
        
        public int difficultyRating;

        public List<Adventurer> assignedAdventurers = new List<Adventurer>();
    }
}