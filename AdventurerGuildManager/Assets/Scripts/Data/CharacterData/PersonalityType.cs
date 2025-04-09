using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Data.CharacterData
{
    [Serializable]
    [CreateAssetMenu(menuName = "Kwest/ Personality Type")]
    public class PersonalityType: ScriptableObject
    {

        public List<string> possibleDialogs = new List<string>();
        
        [HideInInspector] public string GUID;
        
        [ContextMenu("Generate GUID")]
        private void GenerateID()
        {
            GUID = System.Guid.NewGuid().ToString();
        }

    }
}