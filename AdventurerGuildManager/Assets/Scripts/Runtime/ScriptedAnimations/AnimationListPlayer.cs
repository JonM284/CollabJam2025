using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ScriptedAnimations
{
    public class AnimationListPlayer : AnimationsBase
    {

        #region Serialized Fields

        [SerializeField] private List<AnimationsBase> m_animations = new List<AnimationsBase>();

        #endregion
        
        public override void SetProgress(float progress)
        {
            m_animations.ForEach(a => a.SetProgress(progress));
        }
        
        public override void SetInitialValues()
        {
            m_animations.ForEach(a => a.SetInitialValues());
        }

        public override void SetAnimationValue(float progress)
        {
            m_animations.ForEach(a => a.SetAnimationValue(progress));
        }

        public override void ChangePingPongVariables()
        {
            m_animations.ForEach(a => a.ChangePingPongVariables());
        }
    }
}