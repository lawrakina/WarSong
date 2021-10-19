using Code.Data;
using Code.Data.Unit;
using Code.Unit.Data;


namespace Code.Unit {

	public class UnitBattle {
		private readonly AttackCharacteristicCoeffsData _attackCharacteristicCoeffsData;
		public float AttackValue { get;  private set; }


		public UnitBattle(BasicCharacteristics characteristics, AttackCharacteristicCoeffsData attackCharacteristicCoeffsData) {
			_attackCharacteristicCoeffsData = attackCharacteristicCoeffsData;
			UpdateAttackPower(characteristics);
		}

		public void UpdateAttackPower(BasicCharacteristics characteristics) {
			AttackValue = characteristics.Values.Strength * _attackCharacteristicCoeffsData.GetAttackCharacteristicCoeff(characteristics.CharacterClass, BasicCharacteristicType.Strength)
			              + characteristics.Values.Agility * _attackCharacteristicCoeffsData.GetAttackCharacteristicCoeff(characteristics.CharacterClass, BasicCharacteristicType.Agility)
			              + characteristics.Values.Stamina * _attackCharacteristicCoeffsData.GetAttackCharacteristicCoeff(characteristics.CharacterClass, BasicCharacteristicType.Stamina)
			              + characteristics.Values.Intellect * _attackCharacteristicCoeffsData.GetAttackCharacteristicCoeff(characteristics.CharacterClass, BasicCharacteristicType.Intelect)
			              + characteristics.Values.Spirit * _attackCharacteristicCoeffsData.GetAttackCharacteristicCoeff(characteristics.CharacterClass, BasicCharacteristicType.Spirit);
		}
	}

}