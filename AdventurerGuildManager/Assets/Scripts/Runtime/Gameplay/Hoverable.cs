using System;
using Runtime.GameControllers;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Gameplay
{
    public class Hoverable: MonoBehaviour
    {
        
        #region Events

        public UnityEvent onHoverEnter;

        public UnityEvent onHoverExit;
        
        public UnityEvent onPress;

        #endregion

        #region Serialized Fields

        [SerializeField] private HoverableTypes m_hoverableType;

        #endregion

        #region Unity Events

        public void OnMouseEnter()
        {
            if (!InteractionGameManager.Instance.isHoldingObject)
            {
                return;
            }

            InteractionGameManager.Instance.SetCurrentHovered(this);
            onHoverEnter?.Invoke();
        }

        public void OnMouseExit()
        {
            if (!InteractionGameManager.Instance.isHoldingObject)
            {
                return;
            }
            
            InteractionGameManager.Instance.ResetCurrentHovered();
            onHoverExit?.Invoke();
        }

        public void OnMouseDown()
        {
            if (InteractionGameManager.Instance.isHoldingObject)
            {
                return;
            }
            
            onPress?.Invoke();
        }

        #endregion
        
        #region Class Implementation

        public HoverableTypes GetHoverableType() => m_hoverableType;

        #endregion



    }
}