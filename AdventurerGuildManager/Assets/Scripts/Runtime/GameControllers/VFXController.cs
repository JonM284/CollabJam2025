using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Project.Scripts.Utils;
using Runtime.GameControllers;
using Runtime.VFX;
using UnityEngine;
using Utils;

namespace GameControllers
{
    public class VFXController: GameControllerBase
    {

        #region Static

        public static VFXController Instance { get; private set; }

        #endregion

        #region Serialized Fields

        [SerializeField] private CommonVFXData commonVFXData;

        #endregion
        
        #region Private Fields

        private Transform m_vfxPool;
        
        private Transform m_enabledVFXPool;

        private List<VFXPlayer> m_cached_VFX = new List<VFXPlayer>();

        private List<VFXPlayer> m_active_VFX = new List<VFXPlayer>();

        #endregion
        
        #region Accessors

        public Transform vfxPool => CommonUtils.GetRequiredComponent(ref m_vfxPool, () => TransformUtils.CreatePool(transform, false));

        #endregion

        #region Controller Inherited Fields
        
        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
            base.Initialize();
        }

        public override void Cleanup()
        {
            m_cached_VFX.ForEach(c => Destroy(c.gameObject));
            m_cached_VFX.Clear();
            m_active_VFX.ForEach(c => Destroy(c.gameObject));
            m_active_VFX.Clear();
            if (vfxPool != null && vfxPool.childCount > 0)
            {
                for (var n = 0; n < vfxPool.childCount; ++n)
                {
                    Transform temp = vfxPool.GetChild(n);
                    GameObject.Destroy(temp.gameObject);
                }   
            }
            base.Cleanup();
        }

        #endregion

        #region Unity Events
        
        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        void LateUpdate()
        {
            if (m_active_VFX.Count == 0)
            {
                return;
            }
            
            if (m_active_VFX.Count > 0)
            {
                var currentActive = Enumerable.ToList(m_active_VFX);
                currentActive.ForEach(vfx => vfx.CheckVFX());
            }
        }

        #endregion
        
        #region Class Implementation

        public void ForceDeathVFX(Vector3 _deathPosition)
        {
            PlayAt(commonVFXData.deathVFXPrefab, _deathPosition, Quaternion.identity);
        }

        public void PreloadCommonVFX(int _amount = 4)
        {

            for (int i = 0; i < _amount; i++)
            {
                m_cached_VFX.Add(Instantiate(commonVFXData.damageVFXPrefab, vfxPool));
                m_cached_VFX.Add(Instantiate(commonVFXData.buffVFXPrefab, vfxPool));
                m_cached_VFX.Add(Instantiate(commonVFXData.debuffVFXPrefab, vfxPool));
            }
            
            
        }

        public void ForceVFXPreload(VFXPlayer _player, int _amount = 1)
        {
            for (int i = 0; i < _amount; i++)
            {
                m_cached_VFX.Add(Instantiate(_player, vfxPool));
            }
        }

        /// <summary>
        /// Return incoming vfx to a pool to avoid constant instantiation
        /// </summary>
        /// <param name="vfxPlayer"></param>
        public void ReturnToPool(VFXPlayer vfxPlayer)
        {
            if (vfxPlayer == null)
            {
                return;
            }

            if (m_active_VFX.Contains(vfxPlayer))
            {
                m_active_VFX.Remove(vfxPlayer);
            }
            
            m_cached_VFX.Add(vfxPlayer);
            vfxPlayer.Stop();
            vfxPlayer.transform.ResetTransform(vfxPool);
        }

        public void PlayAt(VFXPlayer vfxPlayer, Vector3 position, Quaternion rotation, Transform activeParent = null)
        {
            if (vfxPlayer.IsNull())
            {
                Debug.Log("No VFX Player Attached");
                return;
            }

            
            var foundVFX = m_cached_VFX.FirstOrDefault(c => c.vfxplayerIdentifier == vfxPlayer.vfxplayerIdentifier);

            if (!foundVFX)
            {
                foundVFX = Instantiate(vfxPlayer);
            }
            else
            {
                m_cached_VFX.Remove(foundVFX);
            }

            foundVFX.transform.parent = !activeParent.IsNull() ? activeParent : null;
            foundVFX.transform.position = position;
            foundVFX.transform.rotation = rotation;

            foundVFX.Play();
            
            m_active_VFX.Add(foundVFX);
        }

        public void PlayBuffDebuff(bool _isBuff, Vector3 position, Quaternion rotation, Transform activeParent = null)
        {
            PlayAt(_isBuff ? commonVFXData.buffVFXPrefab : commonVFXData.debuffVFXPrefab, position, rotation,
                activeParent);
        }

        public void PlayDamageVFX(Vector3 position, Quaternion rotation, Transform activeParent = null)
        {
            PlayAt(commonVFXData.damageVFXPrefab, position, rotation, activeParent);
        }

        #endregion


    }
}