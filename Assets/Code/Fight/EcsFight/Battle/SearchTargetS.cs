using System.Collections.Generic;
using System.Linq;
using Code.Fight.EcsFight.Settings;
using Code.Profile.Models;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Battle{
    public class SearchTargetS : IEcsInitSystem, IEcsRunSystem{
        private EcsWorld _world = null;
        private InOutControlFightModel _model;
        private EcsFilter<UnitC> _searchInitFilter;
        private EcsFilter<UnitC, TargetListC, NeedFindTargetTag> _searchFilter;

        public void Init(){
            foreach (var i in _searchInitFilter){
                ref var entity = ref _searchInitFilter.GetEntity(i);
                ref var list = ref entity.Get<TargetListC>();
                list.List = new List<GameObject>();
            }
        }

        public void Run(){
            foreach (var i in _searchFilter){
                ref var entity = ref _searchInitFilter.GetEntity(i);
                ref var unit = ref _searchFilter.Get1(i);
                ref var targets = ref _searchFilter.Get2(i);
                // ref var currentTarget = ref entity.Get<CurrentTargetC>();
                targets.List = unit.UnitVision.Visor.DetectedObjectsOrderedByDistance;
                var listOfTargets = unit.UnitVision.Visor.DetectedObjectsOrderedByDistance;
                targets.Current = listOfTargets.Count > 0 ? listOfTargets.First() : null;
                if (targets.IsExist){
                    targets.SqrDistance =
                        (targets.Current.transform.position - unit.Transform.position).sqrMagnitude;
                    // currentTarget.Value.transform.localScale = Vector3.one * 2;
                }

                entity.Del<NeedFindTargetTag>();
            }
        }
    }
}