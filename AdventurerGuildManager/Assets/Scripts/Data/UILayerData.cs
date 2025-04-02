using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Dodge-ball/UI/UI Layer")]
    public class UILayerData: ScriptableObject
    {
        public string guid;

        [ContextMenu("Generate GUID")]
        private void GenerateID()
        {
            guid = System.Guid.NewGuid().ToString();
        }
    }
}