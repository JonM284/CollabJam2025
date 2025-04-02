using System.Collections.Generic;
using Project.Scripts.Utils;
using Runtime.CameraBehaviour;
using UnityEngine;

namespace Utils
{
    public static class CameraUtils
    {

        #region Private Fields

        private static UnityEngine.Camera m_mainCamera;

        private static CameraPositionTracker m_cameraPositionTracker;

        private static CameraZoomTracker m_cameraZoomTracker;
        
        #endregion

        #region Accessors

        private static UnityEngine.Camera mainCamera => CommonUtils.GetRequiredComponent(ref m_mainCamera, () =>
        {
            var c = UnityEngine.Camera.main;
            return c;
        });

        private static CameraPositionTracker cameraPositionTracker => CommonUtils.GetRequiredComponent(
            ref m_cameraPositionTracker,
            () =>
            {
                var cpt = mainCamera.GetComponentInParent<CameraPositionTracker>();
                return cpt;
            });
        
        private static CameraZoomTracker cameraZoomTracker => CommonUtils.GetRequiredComponent(
            ref m_cameraZoomTracker,
            () =>
            {
                var cpt = mainCamera.GetComponentInParent<CameraZoomTracker>();
                return cpt;
            });

        #endregion

        #region Class Implementation

        public static UnityEngine.Camera GetMainCamera()
        {
            return mainCamera;
        }

        public static void SetCameraTrackPos(Vector3 _trackPos, bool _isLocked)
        {
            cameraPositionTracker.SetTrackCamera(_trackPos, _isLocked);
        }
        
        public static void SetCameraTrackPos(Transform _trackPos, bool _isLocked)
        {
            cameraPositionTracker.SetTrackCamera(_trackPos, _isLocked);
        }

        public static void SetCameraTrackPosCentral(List<Transform> _transforms, bool _isLocked)
        {
            cameraPositionTracker.SetCentralPosition(_transforms, _isLocked);
        }

        public static void SetCameraZoom(float _zoomPercentage)
        {
            cameraZoomTracker.SetNewValue(_zoomPercentage);
        }
        
        #endregion
        
        
    }
}