using UnityEngine;

namespace Runtime.ScriptedAnimations.RectTransform
{
    public abstract class RectTransformAnimation: AnimationsBase
    {
        
        #region Serialized Fields

        [SerializeField] private UnityEngine.RectTransform _target;

        #endregion
        
        #region Accessors

        protected UnityEngine.RectTransform target => _target;

        #endregion
        
    }
}