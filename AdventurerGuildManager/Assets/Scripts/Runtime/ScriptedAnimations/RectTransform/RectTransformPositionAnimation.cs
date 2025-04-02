using UnityEngine;

namespace Runtime.ScriptedAnimations.RectTransform
{
    public class RectTransformPositionAnimation: RectTransformAnimation
    {
        
        #region Serialized Fields

        [SerializeField] private Vector2 m_from;
        
        [SerializeField] private Vector2 m_to;
        
        #endregion

        #region Private Fields

        private Vector2 m_currentFrom;

        private Vector2 m_currentTo;

        #endregion

        public override void SetAnimationValue(float progress)
        {
            target.anchoredPosition = Vector3.LerpUnclamped(m_currentFrom, m_currentTo, progress);
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