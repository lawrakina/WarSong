using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Player
{
    public sealed class MovementPlayer4RotateTransformSystem : IEcsRunSystem
    {
        private EcsFilter<NeedRotateComponent, PlayerComponent, RotateSpeed> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var needRotate = ref _filter.Get1(i);
                ref var rootTransform = ref _filter.Get2(i).rootTransform;
                ref var modelTransform = ref _filter.Get2(i).rootTransform;
                ref var rotateSpeed = ref _filter.Get3(i).value;


                // rootTransform.rotation = Quaternion.Slerp(rootTransform.rotation, needRotate.value.rotation, Time.deltaTime * rotateSpeed);

                // Вращает указанный вектор в сторону цели 
                // Vector3 newDir = Vector3.RotateTowards(
                //     rootTransform.forward, 
                //     needRotate.value.position, 
                //     rotateSpeed * Time.deltaTime, 0.0f);
                // // Создание поворота
                // rootTransform.rotation = Quaternion.LookRotation(newDir);

                // Dbg.Log($"111{needRotate.value.x}");
                rootTransform.RotateAround(
                    rootTransform.position,
                    Vector3.up,
                    rotateSpeed * Time.fixedDeltaTime * needRotate.value.localPosition.x
                );
                // rootTransform.RotateAround(
                //     rootTransform.position + needRotate.value,
                //     Vector3.up,
                //     rotateSpeed * Time.fixedDeltaTime * needRotate.value.x
                // );

                entity.Del<NeedRotateComponent>();
            }
        }
    }
}