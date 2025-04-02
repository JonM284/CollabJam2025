using Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.ScriptedAnimations.RectTransform
{
    public class TmpTextColorAnimation: RectTransformAnimation
    {
        #region Serialized Fields

        [SerializeField] private Color m_from;
        
        [SerializeField] private Color m_to;
        
        #endregion

        #region Private Fields

        private TMP_Text m_targetText;

        private Color m_currentFrom;

        private Color m_currentTo;

        #endregion

        #region Accessors

        public TMP_Text targetText => CommonUtils.GetRequiredComponent(ref m_targetText, () =>
        {
            var i = target.GetComponent<TMP_Text>();
            return i;
        });

        #endregion
        
        public override void SetAnimationValue(float progress)
        {
            targetText.color = Color.LerpUnclamped(m_currentFrom, m_currentTo, progress);
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