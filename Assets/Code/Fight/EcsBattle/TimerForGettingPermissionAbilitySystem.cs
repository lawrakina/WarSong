using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle {

	public class TimerForGettingPermissionAbilitySystem : IEcsRunSystem {
		private EcsFilter<BattleInfoMainWeaponComponent>.Exclude<PermissionForKnockbackAbilityAllowedComponent,
				AwaitTimerForKnockbackAbilityComponent> _filter;

		public void Run() {
			foreach (var i in _filter) {
				ref var entity = ref _filter.GetEntity(i);
				ref var weapon = ref _filter.Get1(i);

				if (!entity.Has<TimerTickForGetPermissionToKnockbackAbilityComponent>() ) {
					//timer not found, start timer
					ref var timer = ref entity.Get<TimerTickForGetPermissionToKnockbackAbilityComponent>();
					timer._estimatedTime = weapon._attackValue.GetAttackSpeed();
				} else {
					//timer founded, increment ValueTimer
					ref var timer = ref entity.Get<TimerTickForGetPermissionToKnockbackAbilityComponent>();
					timer._estimatedTime -= Time.deltaTime;
					if (timer._estimatedTime <= 0) {
						entity.Del<TimerTickForGetPermissionToKnockbackAbilityComponent>();
						entity.Get<PermissionForKnockbackAbilityAllowedComponent>();
					}
				}
			}
		}
	}

}