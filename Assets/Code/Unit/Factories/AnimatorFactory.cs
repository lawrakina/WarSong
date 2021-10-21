using Code.Data.Unit;
using UnityEngine;


namespace Code.Unit.Factories{
    public sealed class AnimatorFactory{
        private readonly CharacterData _settings;

        public AnimatorFactory(CharacterData settings){
            _settings = settings;
        }

        public Animator GenerateAnimator(IPlayerView character, CharacterSettings characterSettings){
            //сейчас аниматор один с одним набором анимаций весит сразу на префабе.
            //ToDo сделать несколько типов анимаций для разных расс, полов, видов гуманоидов
            return character.Transform.GetComponent<Animator>();
        }

        public AnimatorParameters GenerateAnimatorParameters(IPlayerView character, CharacterSettings characterSettings){
            character.AnimatorParameters.WeaponType = character.UnitEquipment.GetWeaponType();
            return character.AnimatorParameters;
        }
    }
}