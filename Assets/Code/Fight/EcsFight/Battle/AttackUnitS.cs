using Code.Extension;
using Code.Fight.EcsFight.Input;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Battle{
    public class AttackUnitS : IEcsInitSystem, IEcsRunSystem{
        private readonly int _bulletCapacity;
        private EcsFilter<UnitC> _units;
        private EcsFilter<UnitC, NeedAttackTargetC, TargetListC> _filter;

        public AttackUnitS(int bulletCapacity){
            _bulletCapacity = bulletCapacity;
        }

        public void Init(){
            foreach (var i in _units){
                // создание магазина патронов 
            }
        }

        public void Run(){
            foreach (var i in _filter){
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get1(i);
                ref var target = ref _filter.Get3(i);
                if (target.IsExist){
                    ref var moveEvent = ref entity.Get<ManualMoveEventC>();
                    moveEvent.ControlType = ControlType.AutoAttack;
                    
                    //бежим к цели
                    if (target.Current.transform.SqrDistance(unit.Transform) >= unit.Weapons.SqrDistance){
                        var direction = target.Current.transform.position - unit.Transform.position;
                        moveEvent.Vector = direction;
                        // DebugExtension.DebugArrow(unit.Transform.position, direction, Color.red);
                        if (entity.Has<CameraC>()){
                            var camera = entity.Get<CameraC>();
                            moveEvent.CameraRotation = camera.Value.Transform.rotation;
                        }
                        var move = new AutoMoveEventC{
                            Vector = new Vector3(
                                direction.x,
                                direction.y,
                                direction.z
                            ),
                        };
                        entity.Get<AutoMoveEventC>() = move;
                    } else{
                        moveEvent.Vector = Vector3.zero;
                        //ударить
                        
                        Dbg.Error($"Attack do not implemented");
                        //
                        entity.Del<NeedAttackTargetC>();
                    }
                } else{
                    entity.Del<NeedAttackTargetC>();
                }
            }
        }
    }
}