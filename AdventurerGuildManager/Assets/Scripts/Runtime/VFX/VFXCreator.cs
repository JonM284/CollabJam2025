using UnityEngine;
using Utils;

namespace Runtime.VFX
{
    public class VFXCreator: MonoBehaviour
    {

        #region Serialized Fields

        [SerializeField] private VFXPlayer vfxToCreate;

        #endregion

        #region Class Implementation

        public void CreateVFX()
        {
            vfxToCreate.PlayAt(transform.position, Quaternion.identity);
        }
        

        #endregion


    }
}