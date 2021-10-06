using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Fight.EcsBattle.Unit.Attack {

	public sealed class Attack7ShootFromMainWeaponSystem : IEcsRunSystem {

		private EcsFilter<
					FinalAttackFromMainWeaponComponent,
					UnitComponent,
					CurrentTargetComponent,
					BattleInfoMainWeaponComponent> _attackers;
		private EcsFilter<WeaponBulletComponent, DisableComponent> _bulletsPool;


		public void Run() {
			foreach (var attacker in _attackers) {
				ref var attackerEntity = ref _attackers.GetEntity(attacker);
				ref var battleInfo = ref _attackers.Get4(attacker);
				ref var attackerTransform = ref _attackers.Get2(attacker)._rootTransform;
				ref var attackerVision = ref _attackers.Get2(attacker)._vision;
				ref var target = ref _attackers.Get3(attacker)._baseUnitView;

				var bulletStartPosition = attackerTransform.position + attackerVision.offsetHead;
				
				var bulletEntity = EcsEntity.Null;
				foreach (var bullet in _bulletsPool) {
					bulletEntity = _bulletsPool.GetEntity(bullet);
				}

				var firedBulletComponent = bulletEntity.Get<WeaponBulletComponent>();
				if (Equals(bulletEntity, EcsEntity.Null)) {
					firedBulletComponent._value = Object.Instantiate(battleInfo._bullet, attackerTransform, true);
					firedBulletComponent._value.name = battleInfo._bullet.name;
				} else {
					bulletEntity.Del<DisableComponent>();
				}

				firedBulletComponent._value.transform.position = bulletStartPosition;
				firedBulletComponent._value.gameObject.SetActive(true);
				firedBulletComponent._value.Target = target;
				firedBulletComponent._value.Clear();
				var targetDirection = target.Transform.position - bulletStartPosition;
				firedBulletComponent._bulletTargetSqrtDistance = (targetDirection).sqrMagnitude;
				
				attackerEntity.Del<FinalAttackFromMainWeaponComponent>();
				ref var attackEffect = ref bulletEntity.Get<WaitingForAttackEffectComponent>();
				attackEffect.AttackEffects ??= new List<IAttackEffect>();
				
				// TODO. Перенести в модели эффектов от абилок
				attackEffect.AttackEffects.Add(new AttackEffect(AttackEffectType.KnockBack, 200f, direction: targetDirection));
			}
		}
	}

}