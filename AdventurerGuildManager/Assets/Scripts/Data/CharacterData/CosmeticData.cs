using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Data.CharacterData
{
    [CreateAssetMenu(menuName = "Kwest/Cosmetics")]
    public class CosmeticData: ScriptableObject
    {

        public List<Color> furColors = new List<Color>();
        public List<Sprite> headCosmetics = new List<Sprite>();
        public List<Sprite> bodyCosmetics = new List<Sprite>();
        public List<Sprite> eyeCosmetics = new List<Sprite>();
        public List<Sprite> eyeBrowCosmetics = new List<Sprite>();
        public List<Sprite> eyeColorCosmetics = new List<Sprite>();
        public List<ArmCoupleData> armCoupleData = new List<ArmCoupleData>();
        public List<AnimalTypeData> animalSpecificCosmetics = new List<AnimalTypeData>();
        public List<Sprite> holdableWeapons = new List<Sprite>();
        public List<Sprite> backWeapons = new List<Sprite>();

    }
}