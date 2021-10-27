using Leopotam.Ecs;


namespace Code.Fight.EcsFight{
    public class UnitBehaviourSettingsS : IEcsRunSystem{
        private EcsWorld _world = null;
        //реакция персонажа на событие ClickEventC
        private EcsFilter<PlayerTag, ClickEventC> _playerClick;
        public void Run(){
            foreach (var i in _playerClick){
                ref var entity = ref _playerClick.GetEntity(i);
                entity.Get<NeedFindTargetTag>();
                entity.Get<NeedAttackTargetC>();
                entity.Del<ClickEventC>();
            }
        }
    }
}