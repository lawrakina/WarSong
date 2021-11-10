using Leopotam.Ecs;


namespace Code.Fight.EcsFight{
    public class UnitBehaviourSettingsS : IEcsRunSystem{
        private EcsWorld _world = null;
        private EcsFilter<PlayerTag, ClickEventC> _clickFilter;

        public void Run(){

            foreach (var i in _clickFilter){
                ref var entity = ref _clickFilter.GetEntity(i);
                entity.Get<NeedFindTargetTag>();
                entity.Get<NeedAttackTargetC>();
                entity.Del<ClickEventC>();
            }
        }
    }
}