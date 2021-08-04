using Code.Fight.EcsBattle.Out.Gui;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Unit.Attack
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
                unit._health.CurrentHp -= damage;
                
                entity.Get<NeedShowUiEventComponent>()._pointsDamage = damage;

                if (unit._health.CurrentHp <= 0.0f)
                {
                    entity.Get<DeathEventComponent>()._killer = infoCollision._value._attacker;
                }

                entity.Del<AttackCollisionComponent>();
            }
        }
    }
}