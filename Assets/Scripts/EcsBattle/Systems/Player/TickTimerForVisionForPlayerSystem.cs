using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.PlayerVision
{
    public class TickTimerForVisionForPlayerSystem : IEcsRunSystem
    {
        private readonly float _time;
        
        private EcsFilter<PlayerComponent, AwaitTimerForVisionComponent> _filter;

        public TickTimerForVisionForPlayerSystem(float time)
        {
            _time = time;
        }

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                ref var timer = ref _filter.Get2(index);
                if (timer.Value < _time)
                {
                    timer.Value += Time.deltaTime;
                }
                else
                {
                    entity.Del<AwaitTimerForVisionComponent>();
                    entity.Get<TimerTickedForVisionComponent>();
                }
            }
        }
    }
}