using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.CameraBehaviour
{
    public class CameraRotationTracker: MonoBehaviour
    {

        #region Serialized Fields
        
        [SerializeField] private float rotationSpeed;

        #endregion

        #region Private Fields

        private Quaternion m_newRotation = Quaternion.identity;

        private Vector3 m_centerPoint;

        private Vector3 m_dirToMidPoint;

        private Vector3 m_originalRotation;

        #endregion
        
    }
}