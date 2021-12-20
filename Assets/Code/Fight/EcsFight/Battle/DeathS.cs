using Code.Extension;
using Code.Fight.EcsFight.Output;
using Code.Fight.EcsFight.Settings;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Battle{
    public class DeathS : IEcsRunSystem{
        private EcsFilter<UnitC, DeathEventC>.Exclude<DeathTag> _deathEvent;
        public void Run(){
            foreach (var i in _deathEvent){
                ref var entity = ref _deathEvent.GetEntity(i);
                ref var unit = ref _deathEvent.Get1(i);
                ref var death = ref _deathEvent.Get2(i);

                unit.Transform.gameObject.tag = TagManager.TAG_OFF;
                // death.Killer.Get<NeedFindTargetTag>();
                unit.Animator.SetDeathTrigger();
                unit.UnitMovement.Motor.enabled = false;
                // entity.Del<UnitC>();
                // entity.Del<EnemyTag>();
                // entity.Del<AnimatorTag>();
                // entity.Del<Weapon<MainHand>>();
                // entity.Del<TargetListC>();
                // entity.Del<AutoMoveEventC>();
                // entity.Del<FoundTargetC>();
                // entity.Del<StartAttackCommand>();
                // entity.Del<NeedAttackTargetCommand>();
                entity.Get<DeathTag>();
                
                if (entity.Has<UiEnemyHealthBarC>()){
                    ref var infoBar = ref entity.Get<UiEnemyHealthBarC>();
                    infoBar.value.SetActive(false);
                }
            }
        }
    }
}