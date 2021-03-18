using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Enemies
{
    public sealed class TimerForCheckVisionForUnitsSystem : IEcsRunSystem
    {
        private readonly float _time;
        private EcsFilter<EnemyComponent>.Exclude<TimerTickedForCheckVisionComponent> _filter;

        public TimerForCheckVisionForUnitsSystem(float time)
        {
            _time = time;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                if (!entity.Has<TimerForCheckVisionForEnemyComponent>())
                {
                    ref var timer = ref entity.Get<TimerForCheckVisionForEnemyComponent>();
                    timer.CurrentTime = Random.Range(0.0f, 0.5f);
                    timer.MaxTime = _time;
                }
                else
                {
                    ref var timer = ref entity.Get<TimerForCheckVisionForEnemyComponent>();
                    timer.CurrentTime += Time.deltaTime;
                    if (timer.CurrentTime > timer.MaxTime)
                    {
                        entity.Del<TimerForCheckVisionForEnemyComponent>();
                        entity.Get<TimerTickedForCheckVisionComponent>();
                    }
                }
            }
        }
    }
}