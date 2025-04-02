using System;

namespace Data
{
    [Serializable]
    public class CustomTimer
    {
        public string timerIdentifier;
        public float currentTime;
        public float maxTime;
        public Action OnFinishAction;
        public bool pauseCondition;
        public bool isFinished = false;

        public CustomTimer(string _timerIdentifier, float _maxTime, bool _pauseCondition, Action _finishAction = null)
        {
            timerIdentifier = _timerIdentifier;
            maxTime = _maxTime;
            currentTime = _maxTime;
            pauseCondition = _pauseCondition;
            OnFinishAction = _finishAction;
            isFinished = false;
        }
    }
}