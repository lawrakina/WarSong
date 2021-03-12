using EcsBattle.Components;
using Leopotam.Ecs;


namespace EcsBattle
{
    public class RotateUiHeathBarsToCameraSystem : IEcsRunSystem
    {
        private EcsFilter<UiEnemyHealthBarComponent> _filter;
        public void Run()
        {
            foreach (var index in _filter)
            {
                _filter.Get1(index).Value.AlignCamera();
            }
        }
    }
}