using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Project.Scripts.Utils;
using UnityEngine;

namespace Runtime.GameControllers
{
    public class TickGameController: GameControllerBase
    {
        
        #region Static

        public static TickGameController Instance { get; private set; }

        #endregion

        #region Actions

        public static event Action Tick;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private float m_tickInterval;

        #endregion

        #region Private Fields

        private float m_lastTickTime;

        private List<CustomTimer> m_customTimers = new List<CustomTimer>();
        
        #endregion

        #region GameControllerBase Inherited Methods
        
        public override void Initialize()
        {
            if (!Instance.IsNull())
            {
                return;
            }
            
            Instance = this;
            m_lastTickTime = Time.time;
            base.Initialize();
        }

        #endregion

        #region Unity Events

        private void Update()
        {
            if (m_customTimers.Count > 0)
            {
                if (m_customTimers.Any(_timer => _timer.isFinished))
                {
                    for (int i = m_customTimers.Count - 1; i > -1; i--)
                    {
                        if (!m_customTimers[i].isFinished)
                        {
                            continue;
                        }
                    
                        m_customTimers.Remove(m_customTimers[i]);
                    }
                }
                
                foreach (var _timer in m_customTimers.Where(_timer => !_timer.pauseCondition))
                {
                    _timer.currentTime -= Time.deltaTime;
                }
                
                foreach (CustomTimer _timer in m_customTimers.Where(_timer => _timer.currentTime <= 0))
                {
                    _timer.OnFinishAction?.Invoke();
                    _timer.isFinished = true;
                }
            }
            
            
            if (!(Time.time - m_lastTickTime >= m_tickInterval))
            {
                return;
            }
            
            Tick?.Invoke();
            m_lastTickTime = Time.time;
        }

        #endregion

        #region Class Implementation

        public void CreateNewTimer(string _timerIdentifier, float _maxTime, bool _pauseCondition = false ,Action _completeAction = null)
        {
            m_customTimers.Add(new CustomTimer(_timerIdentifier, _maxTime, _pauseCondition ,_completeAction));
        }

        #endregion

    }
}