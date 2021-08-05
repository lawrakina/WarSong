using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Statistics
{
    public struct TimerStatisticsObserverComponent
    {
        public float _currentTime;
        public float _maxTime;
        public BattleProgressModel _observer;
    }
    public struct NeedToAddToStatisticsComponent
    {
        public EcsEntity killer;
    }
}