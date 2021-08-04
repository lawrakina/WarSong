using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit
{
    public sealed class RegenerationHealthSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent>.Exclude<CurrentTargetComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get1(i);

                unit._health.CurrentHp += Time.deltaTime;
            }
        }
    }
}