using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Attacks
{
    public sealed class ApplyDamageInUnitSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, AttackCollisionComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get1(i);
                ref var infoCollision = ref _filter.Get2(i);

                var damage = infoCollision._value.Damage;
                unit._currentHpValue -= damage;
                //todo это такой костыль что пиздец
                unit._view.CurrentHp = unit._currentHpValue;
                unit._view.MaxHp = unit._maxHpValue;
                
                entity.Get<NeedShowUiEventComponent>()._pointsDamage = damage;

                if (unit._currentHpValue <= 0.0f)
                {
                    entity.Get<DeathEventComponent>()._killer = infoCollision._value._attacker;
                }

                entity.Del<AttackCollisionComponent>();
            }
        }
    }
}