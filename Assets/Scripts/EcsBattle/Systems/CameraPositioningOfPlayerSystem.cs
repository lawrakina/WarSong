using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle
{
    public sealed class CameraPositioningOfPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<FightCameraComponent, TransformComponent, TargetTransformComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                _filter.Get2(index).Value.position = _filter.Get3(index).Value.position;
            }
        }
    }
}