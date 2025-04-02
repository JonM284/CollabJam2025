using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Project.Scripts.Utils;
using Runtime.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameControllers
{
    public class UIController: GameControllerBase
    {

        #region Static

        public static UIController Instance { get; private set; }

        #endregion

        #region Nested Classes

        [Serializable]
        public class CanvasByLayer
        {
            public UILayerData layer;
            public Canvas associatedCanvas;
        }
        
        [Serializable]
        public class ModalsByLayer
        {
            public UILayerData layer;
            public GameObject modalAssetReference;
        }

        #endregion

        #region Public Fields

        public UnityEvent onBeginFadeIn;

        public UnityEvent onBeginFadeOut;

        #endregion
        
        #region Serialize Fields

        [SerializeField] private ModalsByLayer popupAssetReference;

        [SerializeField] private List<CanvasByLayer> canvasByLayers = new List<CanvasByLayer>();

        [SerializeField] private GameObject textAssetRef;
        
        #endregion

        #region Private Fields

        private List<UIBase> m_activeUIWindows = new List<UIBase>();

        private List<UIBase> m_cachedUIWindows = new List<UIBase>();

        private List<UIPopupDialog> m_cachedPopups = new List<UIPopupDialog>();

        private List<UIPopupDialog> m_activePopups = new List<UIPopupDialog>();

        private Transform m_cachedUIPoolTransform;

        private Transform m_cachedPopupTextPool;
        
        #endregion

        #region Accessors

        public Transform cachedUIPool =>
            CommonUtils.GetRequiredComponent(ref m_cachedUIPoolTransform, ()=>
            {
                var poolTransform = TransformUtils.CreatePool(this.transform, false);
                return poolTransform;
            });
        
        public Transform cachedPopupTextPool =>
            CommonUtils.GetRequiredComponent(ref m_cachedPopupTextPool, ()=>
            {
                var poolTransform = TransformUtils.CreatePool(this.transform, false);
                poolTransform.RenameTransform("Text Pool");
                return poolTransform;
            });

        #endregion

        #region GameControllerBase Inherited Methods

        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
            base.Initialize();
        }

        #endregion

        #region Class Implementation

        public void FadeBlackScreen(bool _fadeIn)
        {
            if (_fadeIn)
            {
                onBeginFadeIn?.Invoke();
            }
            else
            {
                onBeginFadeOut?.Invoke();
            }
        }

        public Transform GetParentCanvasByLayer(UILayerData _layer)
        {
            if (_layer == null)
            {
                return default;
            }

            var _foundCanvasByLayer = canvasByLayers.FirstOrDefault(cbl => cbl.layer == _layer);
            
            return _foundCanvasByLayer.associatedCanvas.transform;
        }

        public void AddUI(UILayerData _layer, GameObject _uiWindow)
        {
            if (_layer == null)
            {
                return;
            }

            var _foundCanvasByLayer = canvasByLayers.FirstOrDefault(cbl => cbl.layer == _layer);
            
            Instantiate(_uiWindow, _foundCanvasByLayer.associatedCanvas.transform);
        }

        public void AddUI(UIWindowData _uiWindowData)
        {
            if (_uiWindowData.IsNull())
            {
                Debug.LogError($"UI window data null");
                return;
            }
            
            Debug.Log($"{_uiWindowData.layerData.name}");
            
            var _cachedWindow = m_cachedUIWindows.FirstOrDefault(ui => ui.uiWindowData == _uiWindowData);
            var _foundCanvasByLayer = canvasByLayers.FirstOrDefault(cbl => cbl.layer.guid == _uiWindowData.layerData.guid);
            
            if (!_cachedWindow.IsNull())
            {
                m_cachedUIWindows.Remove(_cachedWindow);
                _cachedWindow.uiRectTransform.ResetTransform(_foundCanvasByLayer.associatedCanvas.transform);
                m_activeUIWindows.Add(_cachedWindow);
                Debug.Log("Found Window");
                return;
            }

            if (_uiWindowData.uiWindowAssetReference.IsNull())
            {
                Debug.LogError("Asset Reference NULL");
            }

            if (_foundCanvasByLayer.IsNull())
            {
                Debug.LogError("Found canvas null");
            }

            if (_foundCanvasByLayer.associatedCanvas.IsNull())
            {
                Debug.LogError("Associated Canvas null");
            }
            
            Instantiate(_uiWindowData.uiWindowAssetReference, _foundCanvasByLayer.associatedCanvas.transform);
        }

        public void AddUICallback(UIWindowData _uiWindowData, Action<GameObject> _callback)
        {
            StartCoroutine(C_AddUICallback(_uiWindowData, _callback));
        }

        private IEnumerator C_AddUICallback(UIWindowData _uiWindowData, Action<GameObject> _callback)
        {
            var _cachedWindow = m_cachedUIWindows.FirstOrDefault(ui => ui.uiWindowData == _uiWindowData);
            var _foundCanvasByLayer = canvasByLayers.FirstOrDefault(cbl => cbl.layer == _uiWindowData.layerData);
            
            if (_cachedWindow != null)
            {
                m_cachedUIWindows.Remove(_cachedWindow);
                _cachedWindow.uiRectTransform.ResetTransform(_foundCanvasByLayer.associatedCanvas.transform);
                m_activeUIWindows.Add(_cachedWindow);
                Debug.Log("Found Window");
                if (!_callback.IsNull())
                {
                    _callback?.Invoke(_cachedWindow.gameObject);
                }
                yield break;
            }
            
            
            //yield return StartCoroutine(
              //  AddressableController.Instance.C_LoadGameObject(_uiWindowData.uiWindowAssetReference, _callback, _foundCanvasByLayer.associatedCanvas.transform));
        }

        public void ReturnUIToCachedPool(UIBase _uiWindow)
        {
            if (_uiWindow == null)
            {
                return;
            }

            if (_uiWindow is UIPopupDialog popup)
            {
                if (m_activePopups.Contains(popup))
                {
                    m_activePopups.Remove(popup);
                }
                m_cachedPopups.Add(popup);
                popup.uiRectTransform.ResetTransform(cachedUIPool);
                return;
            }
            
            m_cachedUIWindows.Add(_uiWindow);
            _uiWindow.uiRectTransform.ResetTransform(cachedUIPool);
        }

        public void CreatePopup(PopupDialogData _data, Action _confirmAction, Action _closeAction = null)
        {
            var foundPopup = GetCachedUIPopup(_data);
            var _layer = popupAssetReference.layer;
            var _foundCanvasByLayer = canvasByLayers.FirstOrDefault(cbl => cbl.layer == _layer);
            
            //If the popup already exists, use same popup
            if (!foundPopup.IsNull())
            {
                m_cachedPopups.Remove(foundPopup);
                m_activePopups.Add(foundPopup);
                foundPopup.uiRectTransform.ResetTransform(_foundCanvasByLayer.associatedCanvas.transform);
                Debug.Log("Found Popup");
                return;
            }
            
            //Otherwise create a new popup
            object[] arg = {_data, _confirmAction, _closeAction};
            
            var newPopupObject = Instantiate(popupAssetReference.modalAssetReference, _foundCanvasByLayer.associatedCanvas.transform);
            var newPopup = newPopupObject.GetComponent<UIPopupDialog>();
            
            if (!newPopup.IsNull())
            {
                newPopup.AssignArguments(arg);
                m_activePopups.Add(newPopup);
            }
        }

        private UIPopupDialog GetCachedUIPopup(PopupDialogData _dialogData)
        {
            if (_dialogData == null || m_cachedPopups.Count == 0)
            {
                return default;
            }

            var cachedPopup = m_cachedPopups.FirstOrDefault(uip => uip.data == _dialogData);
            return cachedPopup;
        }

        public void CloseAllPopups()
        {
            if (m_activePopups.Count == 0)
            {
                return;
            }

            var newPopuplist = CommonUtils.ToNewList(m_activePopups);

            newPopuplist.ForEach(upd => upd.Close());
        }

        #endregion
        
    }
}