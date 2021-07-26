using System.Collections.Generic;


namespace Profile.Analytic
{
    public interface IAnalyticTools
    {
        void SendMessage(string alias, IDictionary<string, object> eventData = null);
    } 
}