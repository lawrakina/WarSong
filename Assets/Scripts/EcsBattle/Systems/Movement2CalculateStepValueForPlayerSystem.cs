using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class Movement2CalculateStepValueForPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, DirectionMoving, TransformComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var directionMoving = ref _filter.Get2(index).Value;
                ref var playerTransform = ref _filter.Get3(index).Value;
                ref var goTargetTransform = ref _filter.GetEntity(index).Get<GoTargetComponent>().Value.Get<TransformComponent>().Value;

                goTargetTransform.localPosition = directionMoving;

                _filter.GetEntity(index).Get<NeedStepComponent>().Value = playerTransform.position - goTargetTransform.position;
                
                
                //Set position of target direction
                // _filter.GetEntity(index).Get<GoTargetComponent>().Value.Get<TransformComponent>().Value.localPosition =
                    // _filter.Get2(index).Value;
                
                // var target = _unitView.Transform.position - GoTarget.transform.position;
            }
        }
    }

    public struct NeedStepComponent
    {
        public Vector3 Value;
    }
}