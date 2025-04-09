using System;
using MoreMountains.Feedbacks;
using Project.Scripts.Utils;
using Runtime.GameControllers;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Runtime.Gameplay
{
    public class DraggableObject: MonoBehaviour
    {

        #region Serialized Fields

        [SerializeField] private bool m_isAbleToBePlaced;

        [SerializeField] private HoverableTypes m_placementLocations;

        [SerializeField] private float m_sizeSpeed = 5f;

        [SerializeField] private MMF_Player m_changeSizeFeedBack;

        #endregion
        
        #region Private Fields

        private Vector3 m_offset;

        private bool m_isHeld;

        private Camera m_cachedCamera;

        private Vector3 m_screenPoint = Vector3.zero;

        private Vector3 m_shrinkSize = Vector3.one * 0.5f, m_normalSize = Vector3.one;

        private Vector3 m_currentDragPos, m_currentTouchPoint;
        
        #endregion

        #region Accessors

        public bool isSmall { get; private set; }
        
        private Vector3 m_currentFinalSize => isSmall ? m_shrinkSize : m_normalSize;

        public bool canBePlaced => m_isAbleToBePlaced;

        public HoverableTypes placementLoc => m_placementLocations;

        private Camera cam => CommonUtils.GetRequiredComponent(ref m_cachedCamera, CameraUtils.GetMainCamera);

        #endregion

        #region Unity Events

        private void LateUpdate()
        {
            CheckDraggableSize();
        }

        private void OnMouseDown()
        {
            m_isHeld = true;
            InteractionGameManager.Instance.SetCurrentDraggable(this);
            m_offset = transform.position -
                       cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenPoint.z)).FlattenVector3Z();
        }

        private void OnMouseUp()
        {
            InteractionGameManager.Instance.OnDraggableReleased();
            m_isHeld = false;
            SetOriginalSize();
        }

        private void OnMouseDrag()
        {
            if (!m_isHeld)
            {
                return;
            }

            m_currentTouchPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_screenPoint.z);
            m_currentDragPos = cam.ScreenToWorldPoint(m_currentTouchPoint).FlattenVector3Z() + m_offset;
            transform.position = m_currentDragPos;
        }

        #endregion

        #region Class Implementation

        private void CheckDraggableSize()
        {
            if (!m_isHeld)
            {
                return;
            }

            if (Mathf.Approximately(transform.localScale.x, m_currentFinalSize.x))
            {
                return;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, m_currentFinalSize, m_sizeSpeed * Time.deltaTime);
        }

        public void ChangeSize(bool _isShrink)
        {
            isSmall = _isShrink;
            Debug.Log($"Shrink = {_isShrink}");
        }

        public void SetOriginalSize()
        {
            transform.localScale = Vector3.one;
        }

        #endregion
        
        
        
    }
}