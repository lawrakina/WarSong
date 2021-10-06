using UnityEngine;


namespace Code.Fight.EcsBattle.Unit.Attack {

	public interface IAttackEffect {
		AttackEffectType AttackEffectType { get; }
		float Power { get; }
		Vector3 Direction { get; }
	}

}