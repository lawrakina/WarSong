using Code.Fight.EcsBattle.Input;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Unit.Attack
{
    public sealed class Attack1StartProcessSystem : IEcsRunSystem
    {
        private EcsFilter<StartAttackComponent, PermissionForAttackFromMainWeaponAllowedComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);

                entity.Del<PermissionForAttackFromMainWeaponAllowedComponent>();
                entity.Del<StartAttackComponent>();
                entity.Get<NeedFindTargetComponent>();
            }
        }
    }
}