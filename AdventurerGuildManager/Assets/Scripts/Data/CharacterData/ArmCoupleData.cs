using System.Collections.Generic;
using UnityEngine;

namespace Data.CharacterData
{
    [CreateAssetMenu(menuName = "Kwest/Cosmetic Arm Couple")]
    public class ArmCoupleData: ScriptableObject
    {
        public List<Sprite> armCosmetics = new List<Sprite>();
    }
}