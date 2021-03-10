using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Attacks
{
    public sealed class ApplyDamageInUnitSystem : IEcsRunSystem
    {
        private EcsFilter<UnitHpComponent, AttackCollisionComponent> _filter;
    
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unitHp = ref _filter.Get1(i);
                ref var infoCollision = ref _filter.Get2(i);
                
                unitHp.CurrentValue -= infoCollision.Value.Damage;

                if (unitHp.CurrentValue <= 0.0f)
                {
                    entity.Get<DeathEventComponent>()._killer = infoCollision.Value._attacker;
                }
                
                entity.Del<AttackCollisionComponent>();
            }
        }
    }

    public struct DeathEventComponent
    {
        public EcsEntity _killer;
    }
}