using System;
using System.Collections.Generic;
using Project.Scripts.Utils;
using UnityEngine;
using Utils;

namespace Runtime.CameraBehaviour
{
    public class CameraPositionTracker: MonoBehaviour
    {

        #region Serialized Fields

        [SerializeField] private float moveSpeed;
        
        [SerializeField] private float radius;

        [SerializeField] private float padding;

        #endregion

        #region Private Fields

        private float m_timeToTravel;

        private float m_startTime;

        private bool m_isDrag;

        private bool m_isResettingCamera;

        private Transform m_trackedTransform;

        private Vector3 m_trackedPosition;

        private Vector3 m_centralPosition;

        private Vector3 m_dragStartPos;

        private Vector3 m_velocity;

        private Vector3 m_prevLocationBeforeLock;

        private Vector3 m_changeRoomStartPosition;

        private Plane m_dragPlane = new Plane(Vector3.up, Vector3.zero);

        private bool m_isLocked;

        private int playerID;
        
        #endregion

        #region Accessor

        public UnityEngine.Camera mainCamera => CameraUtils.GetMainCamera();

        private Vector3 maxPosition => new Vector3(m_centralPosition.x + padding, 0, m_centralPosition.z + padding);
        
        private Vector3 minPosition => new Vector3(m_centralPosition.x - padding, 0, m_centralPosition.z - padding);

        private Vector3 trackedPosition => !m_trackedTransform.IsNull() ? m_trackedTransform.position : 
            m_trackedPosition != Vector3.zero ? m_trackedPosition : Vector3.zero;
        

        #endregion

        #region Unity Events

        private void Start()
        {
        }

        private void LateUpdate()
        {
            if (!m_isResettingCamera)
            {
                TrackPositionByDrag();
            }
            else
            {
                RecenterCamera();
            }   
            
            if(m_isLocked)
            {
                m_velocity = ConstrainRange(trackedPosition);
            }
            
        }

        #endregion

        #region Class Implementation


        //Check dragging input of the player (mouse)
        private void TrackPositionByDrag()
        {
            if (m_velocity.IsNan())
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if (m_isLocked)
                {
                    UnlockCameraPosition();
                }
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                float entry;
                if (m_dragPlane.Raycast(ray, out entry))
                {
                    m_dragStartPos = ray.GetPoint(entry);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                float entry;
                if (m_dragPlane.Raycast(ray, out entry))
                {
                    var dragCurrentPosition = ray.GetPoint(entry);
                    if ((m_dragStartPos - dragCurrentPosition).magnitude > 0.01)
                    {
                        m_isDrag = true;
                    }
                    m_velocity = ConstrainRange(transform.position + m_dragStartPos - dragCurrentPosition);
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                m_isDrag = false;
            }

            transform.position = Vector3.Lerp(transform.position, m_velocity, moveSpeed * Time.deltaTime);

        }

        //Limit range to current room >> TODO:Add Radius to rooms, some rooms may be bigger
        private Vector3 ConstrainRange(Vector3 _inputVector)
        {
            //Change with current room position, unless camera changes parents
            Vector3 constrainedPos = Vector3.zero;

            var dir = constrainedPos - _inputVector;
            var mag = dir.magnitude;
            
            return new Vector3 {
                x = Mathf.Max(minPosition.x - radius, Math.Min(maxPosition.x + radius, _inputVector.x)),
                y = 0,
                z = Mathf.Max(minPosition.z - radius/2, Math.Min(maxPosition.z + radius/2, _inputVector.z)),
            };
        }

        
        //Automatically move camera to center
        private void RecenterCamera()
        {
            if (m_centralPosition.IsNan())
            {
                return;
            }
            
            var progress = (Time.time - m_startTime) / m_timeToTravel;
            if (progress <= 1) {
                m_velocity = Vector3.Lerp(m_changeRoomStartPosition, m_centralPosition, progress);
            } else {
                m_velocity = m_centralPosition;
                m_isResettingCamera = false;
            }

            transform.position = m_velocity;            
        }

        public void SetTrackCamera(Vector3 _trackPosition, bool _isLocked)
        {
            m_isLocked = _isLocked;
            
            if (m_isLocked)
            {
                m_trackedPosition = _trackPosition;    
            }

            m_velocity = ConstrainRange(_trackPosition);

        }

        public void SetTrackCamera(Transform _trackTransform, bool _isLocked)
        {
            m_isLocked = _isLocked;

            if (m_isLocked)
            {
                m_trackedTransform = _trackTransform;    
            }

            m_velocity = ConstrainRange(_trackTransform.position);
            
        }

        public void SetCentralPosition(List<Transform> _transforms, bool _isLocked)
        {
            m_isLocked = _isLocked;
            var centralPos = GetCentralPosition(_transforms);

            if (m_isLocked)
            {
                m_trackedPosition = centralPos;
            }

            m_velocity = ConstrainRange(centralPos);
        }

        public void ResetCameraBeforeLock()
        {
            if (!m_isLocked)
            {
                return;
            }

            UnlockCameraPosition();
            m_velocity = m_prevLocationBeforeLock;
        }
        
        
        private Vector3 GetCentralPosition(List<Vector3> _transforms){
            Vector3 centralPos = Vector3.zero;
            Vector3 addedPos = Vector3.zero;

            foreach (var pos in _transforms)
            {
                addedPos.x += pos.x;
                addedPos.y += pos.y;
                addedPos.z += pos.z;
            }

            var centerX = addedPos.x / _transforms.Count;
            var centerY = addedPos.y / _transforms.Count;
            var centerZ = addedPos.z / _transforms.Count;

            centralPos = new Vector3(centerX, centerY, centerZ);
            
            return centralPos;
        }
        
        private Vector3 GetCentralPosition(List<Transform> _transforms){
            Vector3 centralPos = Vector3.zero;
            Vector3 addedPos = Vector3.zero;

            foreach (var pos in _transforms)
            {
                addedPos.x += pos.position.x;
                addedPos.y += pos.position.y;
                addedPos.z += pos.position.z;
            }

            var centerX = addedPos.x / _transforms.Count;
            var centerY = addedPos.y / _transforms.Count;
            var centerZ = addedPos.z / _transforms.Count;

            centralPos = new Vector3(centerX, centerY, centerZ);
            
            return centralPos;
        }

        public void UnlockCameraPosition()
        {
            m_isLocked = false;
        }

        #endregion
    }
}