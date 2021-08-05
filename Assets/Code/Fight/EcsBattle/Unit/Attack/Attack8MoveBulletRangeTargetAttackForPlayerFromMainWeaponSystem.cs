using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Attack
{
    public sealed class Attack8MoveBulletRangeTargetAttackForPlayerFromMainWeaponSystem : IEcsRunSystem
    {
        private EcsFilter<WeaponBulletComponent>.Exclude<DisableComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var bullet = ref _filter.Get1(i); 
                var target = _filter.Get1(i)._value.Target;
                var current = bullet._value.transform;
                var targetTransform = bullet._value.Target.Transform;
                var speed = bullet._value.Speed;
                var step = speed * Time.deltaTime;
                
                current.position = Vector3.MoveTowards(current.position,
                    targetTransform.position + new Vector3(0.0f,1.5f,0.0f), step);

                if (Vector3.Distance(current.position, targetTransform.position) < 2.0f)
                {
                    var tempObj = target.Transform.GetComponent<ICollision>();
                    if (tempObj != null)
                    {
                        tempObj.OnCollision(bullet._collision);
                        entity.Get<DisableComponent>();
                        bullet._value.gameObject.SetActive(false);
                        bullet._value.transform.localPosition = Vector3.zero;
                        bullet._value.transform.localRotation = Quaternion.identity;
                    }
                }
            }
        }
    }
}