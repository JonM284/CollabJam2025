using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Runtime.Characters
{
    [Serializable]
    public class KwestCharacterInfo
    {
        public string characterName;

        public bool isQuestGiver;
        public bool isGood;

        public string personalityTypeGUID;

        public int strModifier;
        public int intModifier;
        public int chaModifier;

        public List<string> characterDialog = new List<string>();
        public List<string> characterAcceptedDialog = new List<string>();
        public List<string> characterDeniedDialog = new List<string>();

        public int animalTypeIndex;
        public int furColorCosmeticIndex;
        public int earCosmeticIndex;
        public int eyeCosmeticIndex;
        public int eyeColorCosmeticIndex;
        public int eyeBrowCosmeticIndex;
        public int headCosmeticIndex;
        public int mouthCosmeticIndex;
        public int hairCosmeticIndex;
        public int bodyCosmeticIndex;
        public int armTypeIndex;
        public int leftArmCosmeticIndex, rightArmCosmeticIndex;
        public int numberOfWeapons;
        public int leftWeaponIndex, rightWeaponIndex, backWeaponIndex;
        
        public KwestCharacterInfo(string _name, bool _isQuestGiver ,bool _isGood, string _personalityType, int _str, int _intel, int _cha)
        {
            characterName = _name;
            isGood = _isGood;
            isQuestGiver = _isQuestGiver;
            personalityTypeGUID = _personalityType;
            strModifier = _str;
            intModifier = _intel;
            chaModifier = _cha;
        }
    }
}