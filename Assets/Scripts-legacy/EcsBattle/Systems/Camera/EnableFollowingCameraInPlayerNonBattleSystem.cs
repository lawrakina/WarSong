using EcsBattle.Components;
using EcsBattle.Systems.Input;
using Leopotam.Ecs;


namespace EcsBattle.Systems.Camera
{
    public sealed class EnableFollowingCameraInPlayerNonBattleSystem : IEcsRunSystem
    {
        // private EcsFilter<BaseUnitComponent> _filter;
        public void Run()
        {
            // foreach (var i in _filter)
            // {
            //     ref var entity = ref _filter.GetEntity(i);
            //
            //     if (entity.Has<CurrentTargetComponent>())
            //     {
            //         entity.Del<TimerStopFollowingInPlayerComponent>();
            //     }
            // }
        }
    }
}