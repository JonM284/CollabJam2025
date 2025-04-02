using System;
using Data;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Runtime.UI
{
    public class UIPopupCreator: MonoBehaviour
    {

        #region Serialized Fields

        [SerializeField] private PopupDialogData data;

        [SerializeField] private UnityEvent onConfirmAction;

        [SerializeField] private UnityEvent onCancelAction;
        
        #endregion


        #region Class Implementation

        [ContextMenu("Create Popup")]
        public void CreatePopup()
        {
            if (data == null)
            {
                Debug.LogError("No Data attached to UIPopupCreator");
                return;
            }
            
            Action _confirmAction = null;
            Action _cancelAction = null;

            if (onConfirmAction.GetPersistentEventCount() > 0)
            {
                _confirmAction = onConfirmAction.Invoke;
            }

            if (onCancelAction.GetPersistentEventCount() > 0)
            {
                _cancelAction = onCancelAction.Invoke;
            }
            
            UIUtils.OpenNewPopup(data, _confirmAction, _cancelAction);
        }

        #endregion

    }
}