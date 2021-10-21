using System;
using Code.Extension;
using UnityEngine;


namespace Code.Data.Unit {

	[Serializable]
	// [CreateAssetMenu(fileName = nameof(AttackCharacteristicCoeffsData), menuName = "Configs/" + nameof(AttackCharacteristicCoeffsData))]
	public class AttackCharacteristicCoeffsData : ScriptableObject {
		[Header("Warrior")]
		[SerializeField] private float _strenghtAttackWarriorCoeff;
		[SerializeField] private float _agilityAttackWarriorCoeff;
		[SerializeField] private float _staminaAttackWarriorCoeff;
		[SerializeField] private float _intellectAttackWarriorCoeff;
		[SerializeField] private float _spiritAttackWarriorCoeff;
		[Header("Rogue")]
		[SerializeField] private float _strenghtAttackRogueCoeff;
		[SerializeField] private float _agilityAttackRogueCoeff;
		[SerializeField] private float _staminaAttackRogueCoeff;
		[SerializeField] private float _intellectAttackRogueCoeff;
		[SerializeField] private float _spiritAttackRogueCoeff;
		[Header("Hunter")]
		[SerializeField] private float _strenghtAttackHunterCoeff;
		[SerializeField] private float _agilityAttackHunterCoeff;
		[SerializeField] private float _staminaAttackHunterCoeff;
		[SerializeField] private float _intellectAttackHunterCoeff;
		[SerializeField] private float _spiritAttackHunterCoeff;
		[Header("Mage")]
		[SerializeField] private float _strenghtAttackMageCoeff;
		[SerializeField] private float _agilityAttackMageCoeff;
		[SerializeField] private float _staminaAttackMageCoeff;
		[SerializeField] private float _intellectAttackMageCoeff;
		[SerializeField] private float _spiritAttackMageCoeff;


		public float GetAttackCharacteristicCoeff(CharacterClass characterClass, BasicCharacteristicType characteristicType) {
			switch (characterClass) {
				case CharacterClass.Warrior:
					switch (characteristicType) {
						case BasicCharacteristicType.Strength:
							return _strenghtAttackWarriorCoeff;
						case BasicCharacteristicType.Agility:
							return _agilityAttackWarriorCoeff;
						case BasicCharacteristicType.Stamina:
							return _staminaAttackWarriorCoeff;
						case BasicCharacteristicType.Intelect:
							return _intellectAttackWarriorCoeff;
						case BasicCharacteristicType.Spirit:
							return _spiritAttackWarriorCoeff;
						case BasicCharacteristicType.None:
						default:
							Dbg.Error($"{this}: Characteristic {characteristicType} is not identified");
							return 0;
					}
				case CharacterClass.Rogue:
					switch (characteristicType) {
						case BasicCharacteristicType.Strength:
							return _strenghtAttackRogueCoeff;
						case BasicCharacteristicType.Agility:
							return _agilityAttackRogueCoeff;
						case BasicCharacteristicType.Stamina:
							return _staminaAttackRogueCoeff;
						case BasicCharacteristicType.Intelect:
							return _intellectAttackRogueCoeff;
						case BasicCharacteristicType.Spirit:
							return _spiritAttackRogueCoeff;
						case BasicCharacteristicType.None:
						default:
							Dbg.Error($"{this}: Characteristic {characteristicType} is not identified");
							return 0;
					}
				case CharacterClass.Hunter:
					switch (characteristicType) {
						case BasicCharacteristicType.Strength:
							return _strenghtAttackHunterCoeff;
						case BasicCharacteristicType.Agility:
							return _agilityAttackHunterCoeff;
						case BasicCharacteristicType.Stamina:
							return _staminaAttackHunterCoeff;
						case BasicCharacteristicType.Intelect:
							return _intellectAttackHunterCoeff;
						case BasicCharacteristicType.Spirit:
							return _spiritAttackHunterCoeff;
						case BasicCharacteristicType.None:
						default:
							Dbg.Error($"{this}: Characteristic {characteristicType} is not identified");
							return 0;
					}
				case CharacterClass.Mage:
					switch (characteristicType) {
						case BasicCharacteristicType.Strength:
							return _strenghtAttackMageCoeff;
						case BasicCharacteristicType.Agility:
							return _agilityAttackMageCoeff;
						case BasicCharacteristicType.Stamina:
							return _staminaAttackMageCoeff;
						case BasicCharacteristicType.Intelect:
							return _intellectAttackMageCoeff;
						case BasicCharacteristicType.Spirit:
							return _spiritAttackMageCoeff;
						case BasicCharacteristicType.None:
						default:
							Dbg.Error($"{this}: Characteristic {characteristicType} is not identified");
							return 0;
					}
				case CharacterClass.None:
				default:
					Dbg.Error($"{this}: Class {characterClass} is not identified");
					return 0;
			}
		}
	}

}