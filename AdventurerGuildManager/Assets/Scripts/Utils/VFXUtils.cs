using GameControllers;
using Project.Scripts.Utils;
using Runtime.VFX;
using UnityEngine;

namespace Utils
{
    public static class VFXUtils
    {

        #region Private Fields

        private static VFXController _vfxController;

        #endregion

        #region Accessors

        private static VFXController vfxController => GameControllerUtils.GetGameController(ref _vfxController);

        #endregion

        #region Class Implementation

        /// <summary>
        /// Check to see if this vfxPlayer is done, if it is, return it to the pool for later
        /// </summary>
        /// <param name="vfxPlayer"></param>
        public static void CheckVFX(this VFXPlayer vfxPlayer)
        {
            if (vfxPlayer == null)
            {
                return;
            }

            if (vfxPlayer.is_playing)
            {
                return;
            }
            
            vfxPlayer.ReturnToPool();
        }

        public static void ReturnToPool(this VFXPlayer vfxPlayer)
        {
            Debug.Log($"Returning {vfxPlayer.vfxplayerIdentifier} to pool: {Time.time}");
            vfxController.ReturnToPool(vfxPlayer);
        }

        public static void PlayAt(this VFXPlayer vfxPlayer, Vector3 position, Quaternion rotation, Transform activeParent = null)
        {
            if (vfxPlayer.IsNull())
            {
                return;
            }
            Debug.Log($"playing {vfxPlayer.vfxplayerIdentifier}: {Time.time}");
            vfxController.PlayAt(vfxPlayer, position, rotation, activeParent);
        }

        #endregion

    }
}