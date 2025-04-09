using System;
using System.Collections.Generic;
using NUnit.Framework;
using Runtime.Characters;

namespace Runtime.Quests
{
    [Serializable]
    public class AssignableQuest
    {
        public Quest assignedQuest;
        public List<KwestCharacterInfo> assignedAdventurers = new List<KwestCharacterInfo>();

        public AssignableQuest(Quest _quest)
        {
            assignedQuest = _quest;
            assignedAdventurers = new List<KwestCharacterInfo>();
        }
    }
}