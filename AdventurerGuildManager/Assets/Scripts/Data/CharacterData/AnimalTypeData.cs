using System.Collections.Generic;
using UnityEngine;

namespace Data.CharacterData
{
    [CreateAssetMenu(menuName = "Kwest/Animal Type")]
    public class AnimalTypeData: ScriptableObject
    {
        public List<Sprite> earCosmetic = new List<Sprite>();
        public List<Sprite> hairCosmetic = new List<Sprite>();
        public List<Sprite> mouthCosmetic = new List<Sprite>();
    }
}