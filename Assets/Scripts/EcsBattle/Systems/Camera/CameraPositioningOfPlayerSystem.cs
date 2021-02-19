using System;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    public sealed class CameraPositioningOfPlayerSystem : IEcsRunSystem
    {
        private EcsFilter<FightCameraComponent, TransformComponent, TargetCameraComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                //попытка округления через смещение - не удачно
                // ref var cameraTransform = ref _filter.Get2(index).Value;
                // ref var targetTransform = ref _filter.Get3(index).Position;
                // var sqrDifference = (cameraTransform.position - targetTransform.position).sqrMagnitude;
                // var vectorOffset = new Vector3(0.1f, 0.1f, 0.1f);
                // Dbg.Log($"vectorOffset :{vectorOffset} Magnitude Offset = {vectorOffset.sqrMagnitude}");
                // if (sqrDifference > vectorOffset.sqrMagnitude)
                // {
                //     cameraTransform.position = targetTransform.position;
                // }
                
                _filter.Get2(index).Value.position = _filter.Get3(index).Position.position;
                
                //попытка округлить позицию по осям - неудачно
                // var positionRound = new Vector3(
                //     (float) Math.Round(_filter.Get3(index).Position.position.x, 3),
                //     (float) Math.Round(_filter.Get3(index).Position.position.y, 3),
                //     (float) Math.Round(_filter.Get3(index).Position.position.z, 3)
                // );
                // _filter.Get2(index).Value.position = positionRound;
            }
        }
    }
}