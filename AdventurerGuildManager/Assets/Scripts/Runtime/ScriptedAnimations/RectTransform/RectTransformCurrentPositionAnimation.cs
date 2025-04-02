using UnityEngine;

namespace Runtime.ScriptedAnimations.RectTransform
{
    public class RectTransformCurrentPositionAnimation: RectTransformAnimation
    {

        #region Serialized Fields

        [SerializeField] private Vector2 adjustedEndOffset;

        #endregion

        #region Private Fields

        private Vector2 m_startPos;

        #endregion

        #region Accessors

        private Vector2 m_to => m_startPos + adjustedEndOffset;

        #endregion

        #region RectTransformAnimation Inherited Methods

        public override void SetAnimationValue(float progress)
        {
            target.anchoredPosition = Vector3.LerpUnclamped(m_startPos, m_to, progress);
        }
        
        public override void SetInitialValues()
        {
            
        }
        
        public override void ChangePingPongVariables()
        {
            var _oldTo = m_to;

            m_startPos = _oldTo;
        }

        #endregion

        #region Class Implementation

        public void Initialize()
        {
            m_startPos = target.anchoredPosition;
            Play();
        }

        #endregion

    }
}