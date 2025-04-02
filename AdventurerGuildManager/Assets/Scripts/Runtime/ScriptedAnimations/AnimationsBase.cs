using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.ScriptedAnimations
{
    public abstract class AnimationsBase: MonoBehaviour
    {

        #region Events

        public UnityEvent OnAnimationFinished;

        #endregion
        
        #region Serialized Fields

        [SerializeField] protected float m_maxTime = 1;

        [SerializeField] protected bool m_isContinuous;
        
        [SerializeField] protected AnimationCurve m_curve = AnimationCurve.Linear(0,0,1,1);

        #endregion

        #region Private Fields

        private float m_startTime;

        private float m_progress;
        
        private bool m_isPlaying;
        
        private bool m_isPingPong;

        private bool m_isLoop;

        private int amountOfTimesPerformed;
        
        #endregion

        #region Accessors

        public bool isPlaying => m_isPlaying;

        #endregion

        #region Class Implementation

        private IEnumerator ApplyAnimation()
        {
            while (m_isPlaying)
            {
                m_progress = (Time.time - m_startTime) / m_maxTime;
                                    
                SetProgress(m_progress);
                
                if (m_isPlaying && m_progress >= 0.98)
                {
                    if (!m_isPingPong && !m_isLoop)
                    {
                        OnAnimationFinished?.Invoke();
                        SetProgress(1);
                        m_isPlaying = false;
                        yield break;
                    } 
                    
                    if (m_isPingPong)
                    {
                        m_startTime = Time.time;
                        ChangePingPongVariables();
                        if (!m_isContinuous)
                        {
                            m_isPingPong = false;
                        }
                    }else if (m_isLoop)
                    {
                        m_startTime = Time.time;
                        if (!m_isContinuous)
                        {
                            m_isLoop = false;
                        }
                    }

                }
                yield return null;
            }
        }

        public void Play()
        {
            m_startTime = Time.time;
            m_isPlaying = true;
            SetInitialValues();
            StartCoroutine(ApplyAnimation());
        }

        public void PlayPingPong()
        {
            m_startTime = Time.time;
            m_isPlaying = true;
            m_isPingPong = true;
            SetInitialValues();
            StartCoroutine(ApplyAnimation());
        }

        public void PlayLoop()
        {
            m_startTime = Time.time;
            m_isPlaying = true;
            m_isLoop = true;
            SetInitialValues();
            StartCoroutine(ApplyAnimation());
        }

        public void PlayReverse()
        {
            m_startTime = Time.time;
            m_isPlaying = true;
            ChangePingPongVariables();
            StartCoroutine(ApplyAnimation());
        }

        public void Stop()
        {
            m_isPlaying = false;
            m_isLoop = false;
            m_isPingPong = false;
            StopCoroutine(ApplyAnimation());
        }

        public virtual void SetProgress(float progress)
        {
            SetAnimationValue(m_curve.Evaluate(progress));
        }

        public abstract void SetInitialValues();
        
        public abstract void SetAnimationValue(float progress);

        public abstract void ChangePingPongVariables();

        #endregion

    }
}