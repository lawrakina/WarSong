using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight.Battle{
    public class WeaponsInUnitS : IEcsRunSystem{
        private EcsWorld _world = null;
        private EcsFilter<WeaponC>.Exclude<DisableTag> _filter;
        public void Run(){
            foreach (var i in _filter){
                ref var entity = ref _filter.GetEntity(i);
                ref var weapon = ref _filter.Get1(i);
                if (weapon.CanUse) continue;
                if (weapon.CurrentCooldownTime < weapon.MaxCooldownTime)
                    weapon.CurrentCooldownTime += Time.deltaTime;
                else
                    weapon.CanUse = true;
            }
        }
    }
}