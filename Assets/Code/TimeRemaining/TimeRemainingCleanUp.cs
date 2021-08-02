using System;
using System.Collections.Generic;


namespace Code.TimeRemaining
{
    public sealed class TimeRemainingCleanUp : IDisposable
    {
        #region Fields

        private readonly List<ITimeRemaining> _timeRemainings;

        #endregion


        #region ClassLifeCycles

        public TimeRemainingCleanUp()
        {
            _timeRemainings = TimeRemainingExtensions.TimeRemainings;
        }

        #endregion


        public void Dispose()
        {
            _timeRemainings.Clear();
        }
    }
}