using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Ability {

	public sealed class AttackAbility6StartTimerLagBeforeAbilitySystem : IEcsRunSystem {
		private EcsFilter<NeedKnockbackAbilityComponent,BattleInfoMainWeaponComponent> _filter;
		public void Run()
		{
			foreach (var i in _filter)
			{
				ref var entity = ref _filter.GetEntity(i);
				ref var battleInfo = ref _filter.Get2(i);

				if (!entity.Has<AwaitTimerForKnockbackAbilityComponent>())
				{
					ref var timer = ref entity.Get<AwaitTimerForKnockbackAbilityComponent>();
					timer._estimatedTime = battleInfo._attackValue.GetTimeLag();
				}
				else
				{
					//increment timer
					ref var timer = ref entity.Get<AwaitTimerForKnockbackAbilityComponent>();
					timer._estimatedTime -= Time.deltaTime;
					if (timer._estimatedTime <= 0)
					{
						entity.Del<AwaitTimerForKnockbackAbilityComponent>();
						entity.Del<NeedKnockbackAbilityComponent>();
						entity.Get<FinalKnockbackAbilityFromMainWeaponComponent>();
					}
				}
			}
		}
	}

}