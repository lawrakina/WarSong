using Code.Unit;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Unit.Attack
{
    public sealed class Attack7FinalTargetAttackForPlayerFromMainWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<
            FinalAttackFromMainWeaponComponent,
            PlayerComponent,
            CurrentTargetComponent,
            BattleInfoMainWeaponComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var target = ref _filter.Get3(i);
                ref var battleInfo = ref _filter.Get4(i);

                var targetCollision = target._baseUnitView.Transform.GetComponent<ICollision>();
                var collision =
                    new InfoCollision(battleInfo._attackValue.GetAttackAvarage(), entity);
                targetCollision?.OnCollision(collision);

                entity.Del<FinalAttackFromMainWeaponComponent>();
            }
        }
    }
}