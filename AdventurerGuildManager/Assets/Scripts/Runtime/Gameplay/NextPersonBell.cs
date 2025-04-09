using System;
using Runtime.GameControllers;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Gameplay
{
    public class NextPersonBell: MonoBehaviour
    {

        public UnityEvent onBellPressed;
        
        private void OnMouseDown()
        {
            if (InteractionGameManager.Instance.isCurrentlyInteracting)
            {
                return;
            }
            
            onBellPressed?.Invoke();
        }
    }
}