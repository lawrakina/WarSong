using Code.Extension;
using Code.Fight.EcsFight.Battle;
using Code.Fight.EcsFight.Output;
using Code.Fight.EcsFight.Settings;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight{
    public class DeathS : IEcsRunSystem{
        private EcsFilter<UnitC, DeathEventC> _deathEvent;
        public void Run(){
            foreach (var i in _deathEvent){
                ref var entity = ref _deathEvent.GetEntity(i);
                ref var unit = ref _deathEvent.Get1(i);
                ref var death = ref _deathEvent.Get2(i);

                unit.Transform.gameObject.tag = TagManager.TAG_OFF;
                death.Killer.Get<NeedFindTargetTag>();
                unit.Animator.SetDeathTrigger();
                unit.UnitMovement.Motor.enabled = false;
                entity.Del<UnitC>();

                if (entity.Has<UiEnemyHealthBarC>()){
                    ref var infoBar = ref entity.Get<UiEnemyHealthBarC>();
                    infoBar.value.SetActive(false);
                }
            }
        }
    }
}