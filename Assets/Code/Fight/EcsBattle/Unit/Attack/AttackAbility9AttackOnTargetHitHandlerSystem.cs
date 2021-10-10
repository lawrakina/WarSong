using System;
using Code.Extension;
using Code.Fight.EcsBattle.Unit.Attack;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle {

	public class AttackAbility9AttackOnTargetHitHandlerSystem : IEcsRunSystem {
		
		private EcsFilter<
					WeaponBulletComponent,
					WaitingForAttackEffectComponent,
					ReadyForAttackEffectComponent> _hittedBullets;


		public void Run() {
			foreach (var hittedBullet in _hittedBullets) {
				ref var bulletEntity = ref _hittedBullets.GetEntity(hittedBullet);
				ref var attackEffects = ref _hittedBullets.Get2(hittedBullet).AttackEffects;
				var targetView = _hittedBullets.Get1(hittedBullet)._value.Target;

				if (!Equals(attackEffects, null)) {
					foreach (var attackEffect in attackEffects) {
						switch (attackEffect.AttackEffectType) {
							case AttackEffectType.KnockBack:
								targetView.Rigidbody.AddForce(attackEffect.Direction * attackEffect.Power, ForceMode.Impulse);
								break;
							case AttackEffectType.None:
							default:
								Dbg.Error($"{this}: Unknown attack effect type");
								return;
						}
					}
				}				
				bulletEntity.Del<ReadyForAttackEffectComponent>();
				bulletEntity.Del<WaitingForAttackEffectComponent>();
				bulletEntity.Get<DisableComponent>();
			}
		}
	}

}