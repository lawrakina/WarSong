using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Attack {

	public class AttackEffect : IAttackEffect {
		public AttackEffectType AttackEffectType { get; }
		public float Power { get; }
		public Vector3 Direction { get; }


		public AttackEffect(AttackEffectType type, float power = default, Vector3 direction = default) {
			AttackEffectType = type;
			Power = power;
			Direction = direction;
		}
	}

}