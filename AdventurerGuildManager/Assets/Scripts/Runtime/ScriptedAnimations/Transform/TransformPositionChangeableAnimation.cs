using UnityEngine;

namespace Runtime.ScriptedAnimations.Transform
{
    public class TransformPositionChangeableAnimation: TransformAnimation
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
            target.position = Vector3.LerpUnclamped(m_currentFrom, m_currentTo, progress);
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

        public void ChangeEndLocation(Vector3 _newEndLocation)
        {
            m_from = target.position;
            m_to = _newEndLocation;
        }

        public void ChangeStartLocation(Vector3 _newStartLocation)
        {
            m_from = _newStartLocation;
        }
    }
}