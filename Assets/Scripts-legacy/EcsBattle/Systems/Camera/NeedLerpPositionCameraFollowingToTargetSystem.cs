using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Camera
{
    // public sealed class NeedLerpPositionCameraFollowingToTargetSystem : IEcsRunSystem
    // {
    //     private EcsFilter<NeedLerpPositionCameraFollowingToTargetComponent> _filter;
    //
    //     private EcsFilter<
    //         FightCameraComponent,
    //         TransformComponent> _camera;
    //
    //     public void Run()
    //     {
    //         foreach (var i in _filter)
    //         {
    //             ref var entity = ref _filter.GetEntity(i);
    //             ref var timer = ref _filter.Get1(i);
    //
    //             timer.currentTime += Time.deltaTime;
    //             //if timer ended  
    //             if (timer.currentTime > timer.maxTime)
    //             {
    //                 entity.Del<NeedLerpPositionCameraFollowingToTargetComponent>();
    //             }
    //             else
    //             {
    //                 foreach (var c in _camera)
    //                 {
    //                     // ref var cameraSettings = ref _camera.Get1(c);
    //                     ref var camera = ref _camera.Get2(c);
    //                     ref var target = ref _camera.Get1(c);
    //
    //                     camera.value.position = Vector3.Lerp(
    //                         camera.value.transform.position,
    //                         target.positionThirdTarget.position,
    //                         timer.currentTime);
    //                     // cameraSettings.valueToInterpolate);
    //                     // cameraSettings.valueToInterpolate * Time.deltaTime);
    //                 }
    //             }
    //         }
    //     }
    // }
}