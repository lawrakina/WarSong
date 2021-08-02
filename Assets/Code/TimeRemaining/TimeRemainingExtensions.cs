﻿using System.Collections.Generic;


namespace Code.TimeRemaining
{
    public static partial class TimeRemainingExtensions
    {
        #region Fields

        private static readonly List<ITimeRemaining> _timeRemainings = new List<ITimeRemaining>(10);

        #endregion


        #region Properties

        public static List<ITimeRemaining> TimeRemainings => _timeRemainings;

        #endregion


        #region Methods

        public static void AddTimeRemaining(this ITimeRemaining value)
        {
            if (_timeRemainings.Contains(value))
            {
                return;
            }

            value.CurrentTime = value.Time;
            _timeRemainings.Add(value);
        }

        public static void RemoveTimeRemaining(this ITimeRemaining value)
        {
            if (!_timeRemainings.Contains(value))
            {
                return;
            }

            _timeRemainings.Remove(value);
        }

        #endregion
    }
}