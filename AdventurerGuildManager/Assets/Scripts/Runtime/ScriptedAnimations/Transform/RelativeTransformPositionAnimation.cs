using UnityEngine;

namespace Runtime.ScriptedAnimations.Transform
{
    public class RelativeTransformPositionAnimation: TransformAnimation
    {
        #region Serialized Fields

        [SerializeField] private Vector3 m_to;

        #endregion
        
        #region Private Fields

        private Vector3 m_startPos;

        #endregion
        
        #region Accessors

        private Vector3 endPos => m_startPos + m_to;

        #endregion

        #region TransformAnimation Inherited Methods

        public override void SetAnimationValue(float progress)
        {
            target.position = Vector3.LerpUnclamped(m_startPos, endPos, progress);
        }
        
        public override void SetInitialValues()
        {
            
        }
        
        public override void ChangePingPongVariables()
        {
            var _oldTo = endPos;

            m_startPos = _oldTo;
        }
        
        #endregion

        #region Class Implementation

        public void Initialize()
        {
            m_startPos = target.position;
            Play();
        }

        #endregion

        
    }
}