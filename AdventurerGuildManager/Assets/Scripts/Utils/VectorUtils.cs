using UnityEngine;

namespace Project.Scripts.Utils
{
    public static class VectorUtils
    {

        #region Class Implementation

        public static Vector3 FlattenVector3Y(this Vector3 _vector)
        {
            _vector.y = 0;
            return _vector;
        }

        public static bool IsNan(this Vector3 vector3)
        {
            return float.IsNaN(vector3.x) || float.IsNaN(vector3.y) || float.IsNaN(vector3.z);
        }
        
        public static Vector3 FlattenVectorToY(this Vector3 _vector, float _desiredY)
        {
            _vector.y = _desiredY;
            return _vector;
        }
        
        
        #endregion
        
    }
}