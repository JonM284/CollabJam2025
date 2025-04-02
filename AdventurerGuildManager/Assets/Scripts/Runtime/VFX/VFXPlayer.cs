using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Utils;
using UnityEngine;

namespace Runtime.VFX
{
    public class VFXPlayer: MonoBehaviour
    {

        #region Public Fields

        public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        #endregion

        #region Serialized Fields

        [SerializeField] private string m_vfx_player_identifier;

        #endregion

        #region Private Fields

        private ParticleSystem.MainModule m_particleSystemProxy;

        #endregion

        #region Accessors

        public bool is_playing => !particleSystems.IsNull() 
                                  && particleSystems.Count > 0 && particleSystems.TrueForAll(ps => ps.isPlaying);

        public string vfxplayerIdentifier => m_vfx_player_identifier;

        #endregion

        #region Class Implementation

        //Play all VFX on this object
        public void Play()
        {
            particleSystems.ForEach(ps => ps.Play());
        }

        //Stop all VFX on this object
        public void Stop()
        {
            particleSystems.ForEach(ps => ps.Stop());
        }

        public void ChangeAllStartColor(Color _newColor)
        {
            if (particleSystems.Count == 0)
            {
                return;
            }


            foreach (var _ps in particleSystems)
            {
                m_particleSystemProxy = _ps.main;
                m_particleSystemProxy.startColor = _newColor;
            }   
            
        }

        #endregion

    }
}