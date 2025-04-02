using Data;
using UnityEngine;
using Utils;

namespace Runtime.UI
{
    public class UIOpener: MonoBehaviour
    {

        #region Serialized Fields

        [SerializeField] private UIWindowData uiWindowData;

        #endregion
        
        #region Class Impelmentation

        public void OpenUIWindow()
        {
            UIUtils.OpenUI(uiWindowData.uiWindowAssetReference, uiWindowData.layerData);
        }

        #endregion
        
    }
}