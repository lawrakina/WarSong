using System.Collections.Generic;


namespace Controller
{
    public static class TimeRemainingExtensions
    {
        #region Fields

        #endregion


        #region Properties

        public static List<ITimeRemaining> TimeRemainings { get; } = new List<ITimeRemaining>(63);

        #endregion


        #region Execute

        public static void AddTimeRemainingExecute(this ITimeRemaining value)
        {
            if (TimeRemainings.Contains(value)) return;

            value.CurrentTime = value.Time;
            TimeRemainings.Add(value);
        }

        public static void RemoveTimeRemainingExecute(this ITimeRemaining value)
        {
            if (!TimeRemainings.Contains(value)) return;
            TimeRemainings.Remove(value);
        }

        #endregion
    }
}