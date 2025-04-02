using System;
using Data;
using Runtime.GameControllers;
using Runtime.UI;
using UnityEngine;

namespace Utils
{
    public static class UIUtils
    {
        
        #region Private Fields

        private static UIController _uiController;

        #endregion

        #region Accessors

        private static UIController uiController => GameControllerUtils.GetGameController(ref _uiController);

        #endregion

        #region Class Implementation

        /// <summary>
        /// Fade Black Screen
        /// </summary>
        /// <param name="_fadeIn">true => Fade into Black, false => Fade to transparent</param>
        public static void FadeBlack(bool _fadeIn)
        {
            uiController.FadeBlackScreen(_fadeIn);
        }

        public static void OpenUI(GameObject _uiWindowAssetRef, UILayerData _layer)
        {
            if (_uiWindowAssetRef == null)
            {
                return;
            }

            uiController.AddUI(_layer, _uiWindowAssetRef);
        }

        public static void OpenUI(UIWindowData _windowData)
        {
            if (_windowData == null)
            {
                return;
            }
            
            uiController.AddUI(_windowData);
        }

        public static void CloseUI(UIBase _uiWindow)
        {
            if (_uiWindow == null)
            {
                return;
            }
            
            uiController.ReturnUIToCachedPool(_uiWindow);
            
        }

        public static void OpenNewPopup(PopupDialogData _data, Action _confirmAction, Action _closeAction = null)
        {
            uiController.CreatePopup(_data, _confirmAction, _closeAction);
        }

        #endregion
        
    }
}