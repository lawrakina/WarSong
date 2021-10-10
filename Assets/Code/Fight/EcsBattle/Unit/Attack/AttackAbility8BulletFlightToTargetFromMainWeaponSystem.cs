using Code.Fight.EcsBattle.Unit.Attack;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle {

	public class AttackAbility8BulletFlightToTargetFromMainWeaponSystem : IEcsRunSystem {

		private EcsFilter<WeaponBulletComponent, WaitingForAttackEffectComponent>.Exclude<DisableComponent> _aimedBullets;

		public void Run() {
			foreach (var aimedBullet in _aimedBullets) {
				ref var bulletEntity = ref _aimedBullets.GetEntity(aimedBullet); 
				ref var bullet = ref _aimedBullets.Get1(aimedBullet);

				var bulletPosition = bullet._value.transform.position;
				var targetPosition = bullet._value.Target.Transform.position;
				bullet._value.transform.position = Vector3.MoveTowards(bulletPosition, targetPosition, bullet._value.Speed * Time.deltaTime);
				if ((bullet._value.Target.Transform.position - bullet._value.transform.position).sqrMagnitude < bullet._bulletTargetSqrtDistance) {
					bullet._bulletTargetSqrtDistance = (bullet._value.Target.Transform.position - bullet._value.transform.position).sqrMagnitude;
				} else {
					bullet._value.gameObject.SetActive(false);
					bulletEntity.Get<ReadyForAttackEffectComponent>();
				}
			}
		}
	}

}