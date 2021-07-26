using System;
using EcsBattle.Components;
using EcsBattle.Systems.Input;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle
{
    // public sealed class CameraPositioningOnMarkerPlayerSystem : IEcsRunSystem
    // {
    //     private EcsFilter<FightCameraComponent, TransformComponent> _filter;
    //         // .Exclude<
    //         //     TimerStopFollowingInPlayerComponent,
    //         //     NeedLerpPositionCameraFollowingToTargetComponent>
    //
    //     public void Run()
    //     {
    //         foreach (var index in _filter)
    //         {
    //             //попытка округления через смещение - не удачно
    //             // ref var cameraTransform = ref _filter.Get2(index).Value;
    //             // ref var targetTransform = ref _filter.Get3(index).Position;
    //             // var sqrDifference = (cameraTransform.position - targetTransform.position).sqrMagnitude;
    //             // var vectorOffset = new Vector3(0.1f, 0.1f, 0.1f);
    //             // Dbg.Log($"vectorOffset :{vectorOffset} Magnitude Offset = {vectorOffset.sqrMagnitude}");
    //             // if (sqrDifference > vectorOffset.sqrMagnitude)
    //             // {
    //             //     cameraTransform.position = targetTransform.position;
    //             // }
    //             
    //             // _filter.Get2(index).value.position = _filter.Get1(index).positionThirdTarget.position;
    //             _filter.Get2(index).value.position = Vector3.Lerp(_filter.Get2(index).value.position,_filter.Get1(index).positionThirdTarget.position, 0.9f);
    //             
    //             //попытка округлить позицию по осям - неудачно
    //             // var positionRound = new Vector3(
    //             //     (float) Math.Round(_filter.Get3(index).Position.position.x, 3),
    //             //     (float) Math.Round(_filter.Get3(index).Position.position.y, 3),
    //             //     (float) Math.Round(_filter.Get3(index).Position.position.z, 3)
    //             // );
    //             // _filter.Get2(index).Value.position = positionRound;
    //         }
    //     }
    // }
}