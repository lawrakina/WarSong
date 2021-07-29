using System.Collections.Generic;
using Interface;
using UnityEngine;


namespace Controller
{
    public sealed class TimeRemainingController : IUpdated
    {
        #region Fields

        private readonly List<ITimeRemaining> _timeRemainingsExecute;

        #endregion


        #region ClassLifeCycles

        public TimeRemainingController()
        {
            _timeRemainingsExecute = TimeRemainingExtensions.TimeRemainings;
        }

        #endregion


        #region IExecute

        public void UpdateTick()
        {
            var time = Time.deltaTime;
            for (var i = 0; i < _timeRemainingsExecute.Count; i++)
            {
                var obj = _timeRemainingsExecute[i];
                obj.CurrentTime -= time;
                if (obj.CurrentTime <= 0.0f)
                {
                    obj?.Method?.Invoke();
                    if (!obj.IsRepeating)
                        obj.RemoveTimeRemainingExecute();
                    else
                        obj.CurrentTime = obj.Time;
                }
            }
        }

        #endregion
    }
}