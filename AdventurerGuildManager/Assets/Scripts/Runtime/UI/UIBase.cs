using System;
using Data;
using Project.Scripts.Utils;
using UnityEngine;
using Utils;

namespace Runtime.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIBase: MonoBehaviour
    {

        #region Events

        protected Action m_confirmAction;

        protected Action m_closeAction;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private UILayerData layer;

        [SerializeField] private UIWindowData uiWindow;

        #endregion
        
        #region Private Fields

        private RectTransform m_uiRectTransform;

        #endregion

        #region Accessors

        public UILayerData uiLayerData => layer;

        public UIWindowData uiWindowData => uiWindow;

        public RectTransform uiRectTransform => CommonUtils.GetRequiredComponent(ref m_uiRectTransform, () =>
        {
            var rt = GetComponent<RectTransform>();
            return rt;
        });

        #endregion

        #region Class Implementation

        public abstract void AssignArguments(params object[] _arguments);

        public void ConfirmAction()
        {
            m_confirmAction?.Invoke();
            Close();
        }

        public void Close()
        {
            m_closeAction?.Invoke();
            UIUtils.CloseUI(this);
        }

        #endregion
        
    }
}