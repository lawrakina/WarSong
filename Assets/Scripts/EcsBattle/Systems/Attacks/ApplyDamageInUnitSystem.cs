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

                var damage = infoCollision._value.Damage;
                unitHp._currentValue -= damage;
                entity.Get<NeedShowUiEventComponent>()._pointsDamage = damage;

                if (unitHp._currentValue <= 0.0f)
                {
                    entity.Get<DeathEventComponent>()._killer = infoCollision._value._attacker;
                }

                entity.Del<AttackCollisionComponent>();
            }
        }
    }
}