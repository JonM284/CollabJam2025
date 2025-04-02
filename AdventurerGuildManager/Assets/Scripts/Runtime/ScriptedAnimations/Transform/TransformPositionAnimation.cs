using UnityEngine;

namespace Runtime.ScriptedAnimations.Transform
{
    public class TransformPositionAnimation : TransformAnimation
    {

        #region Serialized Fields

        [SerializeField] private Vector3 m_from;
        
        [SerializeField] private Vector3 m_to;
        
        #endregion

        #region Private Fields

        private Vector3 m_currentFrom;

        private Vector3 m_currentTo;

        #endregion
        
        public override void SetAnimationValue(float progress)
        {
            target.localPosition = Vector3.LerpUnclamped(m_currentFrom, m_currentTo, progress);
        }
        
        public override void SetInitialValues()
        {
            m_currentFrom = m_from;
            m_currentTo = m_to;
        }

        public override void ChangePingPongVariables()
        {
            m_currentTo = m_from;
            m_currentFrom = m_to;
        }
    }
}