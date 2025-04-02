using UnityEngine;

namespace Runtime.ScriptedAnimations.Transform
{
    public abstract class TransformAnimation : AnimationsBase
    {
        #region Serialized Fields

        [SerializeField] private UnityEngine.Transform _target;

        #endregion
        
        #region Accessors

        protected UnityEngine.Transform target => _target;

        #endregion

        #region AnimationBase Inherited Methods

        

        #endregion
    }
}