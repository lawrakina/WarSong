using System;
using Data;
using Enums;
using Unit.Player;


namespace CharacterCustomizing
{
    public sealed class CustomizerCharacter
    {
        private CharacterData _characterData;

        public CustomizerCharacter(CharacterData characterData)
        {
            _characterData = characterData;
        }

        public void Customize(ref IPlayerView playerView, CharacterSettings settings)
        {
            playerView.CharAttributes.AgroDistance = settings.AgroDistance;
            playerView.CharAttributes.Speed= settings.PlayerMoveSpeed;
            playerView.CharAttributes.RotateSpeedPlayer = settings.RotateSpeedPlayer;

            var person = new PersonCharacter(playerView.Transform.gameObject, _characterData);
            person.CharacterRace = settings.CharacterRace;
            person.CharacterGender = settings.CharacterGender;

            person.Generate();

            var equipmentPoints = new EquipmentPoints(playerView.Transform.gameObject, _characterData);
            equipmentPoints.GenerateAllPoints();
            playerView.EquipmentPoints = equipmentPoints;

            var equipWeapon = new EquipmentWeapon(playerView, settings.Equipment);
            
            
            // switch (settings.CharacterRace)
            // {
            //     case CharacterRace.Human:
            //         person.Race = 
            //         break;
            //
            //     case CharacterRace.NightElf:
            //         break;
            //
            //     case CharacterRace.BloodElf:
            //         break;
            //
            //     case CharacterRace.Orc:
            //         break;
            //
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
            
            
            // var person = new PersonCharacter(playerView.Transform.gameObject, settings);
            
            switch (settings.CharacterClass)
            {
                case CharacterClass.Warrior:
                    playerView.CharacterClass = new CharacterClassWarrior();
                    break;

                case CharacterClass.Rogue:
                    playerView.CharacterClass = new CharacterClassRogue();
                    break;

                case CharacterClass.Hunter:
                    playerView.CharacterClass = new CharacterClassHunter();
                    break;

                case CharacterClass.Mage:
                    playerView.CharacterClass = new CharacterClassMage();
                    break;

                default:
                    throw new Exception(
                        "PlayerFactory. playerData.PlayerSettings.CharacterClass:Недопустимое значение");
            }
        }
    }
}