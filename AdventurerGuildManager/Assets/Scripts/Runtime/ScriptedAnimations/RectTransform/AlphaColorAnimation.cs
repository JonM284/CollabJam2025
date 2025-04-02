using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.ScriptedAnimations.RectTransform
{
    public class AlphaColorAnimation: RectTransformAnimation
    {
        #region Serialized Fields

        [SerializeField] private Color m_from;
        
        [SerializeField] private Color m_to;
        
        #endregion

        #region Private Fields

        private Graphic m_targetImage;

        private Color m_currentFrom;

        private Color m_currentTo;

        private Color m_proxyColor;

        #endregion

        #region Accessors

        public Graphic targetImage => CommonUtils.GetRequiredComponent(ref m_targetImage, () =>
        {
            var i = target.GetComponent<Graphic>();
            return i;
        });

        #endregion
        
        public override void SetAnimationValue(float progress)
        {
            m_proxyColor = targetImage.color;
            m_proxyColor.a = Mathf.Lerp(m_currentFrom.a, m_currentTo.a, progress);
            targetImage.color = m_proxyColor;
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