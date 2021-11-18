using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Settings{
    public class UnitBehaviourSettingsS : IEcsRunSystem{
        private EcsWorld _world = null;
        private EcsFilter<PlayerTag, ClickEventC> _clickFilter;
        private EcsFilter<PlayerTag, SwipeEventC> _swipeFilter;

        public void Run(){
            foreach (var i in _clickFilter){
                ref var entity = ref _clickFilter.GetEntity(i);
                entity.Get<NeedFindTargetTag>();
                entity.Get<NeedAttackTargetC>();
                entity.Del<ClickEventC>();
            }

            foreach (var i in _swipeFilter){
                ref var entity = ref _swipeFilter.GetEntity(i);
                entity.Del<SwipeEventC>();
            }
        }
    }
}