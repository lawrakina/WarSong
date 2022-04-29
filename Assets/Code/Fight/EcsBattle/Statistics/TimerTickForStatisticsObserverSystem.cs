using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Statistics
{
    public sealed class TimerTickForStatisticsObserverSystem : IEcsRunSystem
    {
        private EcsFilter<TimerStatisticsObserverComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);

                timer._currentTime += Time.deltaTime;
                timer._observer.CurrentTimer = timer._currentTime;
            }
        }
    }
}