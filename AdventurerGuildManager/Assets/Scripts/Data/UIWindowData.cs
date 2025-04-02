using System;
using UnityEngine;

namespace Data
{
    [Serializable] 
    [CreateAssetMenu(menuName = "Dodge-ball/UI/Window Dialog Data")]
    public class UIWindowData: ScriptableObject
    {
        public UILayerData layerData;
        
        public GameObject uiWindowAssetReference;
    }
}