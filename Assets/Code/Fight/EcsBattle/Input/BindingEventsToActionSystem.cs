using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Input
{
    public sealed class BindingEventsToActionSystem : IEcsRunSystem
    {
        private EcsFilter<ClickEventComponent> _clickEventFilter;
        private EcsFilter<SwipeEventComponent> _swipeEventFilter;
        public void Run()
        {
            foreach (var i in _clickEventFilter)
            {
                ref var entity = ref _clickEventFilter.GetEntity(i);
                if (entity.Has<PlayerComponent>())
                {
                    entity.Get<StartAttackComponent>();
                    entity.Del<ClickEventComponent>();
                }
            }
            foreach (var i in _swipeEventFilter)
            {
                ref var entity = ref _swipeEventFilter.GetEntity(i);
                if (entity.Has<PlayerComponent>())
                {
                    entity.Get<StartJumpComponent>();
                    entity.Del<SwipeEventComponent>();
                }
            }
        }
    }
}