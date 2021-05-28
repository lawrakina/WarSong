using System;
using Data;
using Enums;
using Extension;
using Unit;
using Unit.Player;
using UnityEngine;


namespace CharacterCustomizing
{
    public sealed class PlayerCustomizerCharacter
    {
        private CharacterData _characterData;

        public PlayerCustomizerCharacter(CharacterData characterData)
        {
            _characterData = characterData;
        }

        public void Customize(IPlayerView playerView, CharacterSettings settings)
        {
            var person = new PersonCharacter(playerView.TransformModel.gameObject, _characterData);
            person.CharacterRace = settings.CharacterRace;
            person.CharacterGender = settings.CharacterGender;
            // switch (settings.CharacterRace)
            // {
            //     case CharacterRace.Human:
            //         person.SkinColor = SkinColor.Human;
            //         break;
            //
            //     case CharacterRace.NightElf:
            //         person.SkinColor = SkinColor.Elf;
            //         break;
            //
            //     case CharacterRace.BloodElf:
            //         person.SkinColor = SkinColor.Elf;
            //         break;
            //
            //     case CharacterRace.Orc:
            //         person.SkinColor = SkinColor.Orc;
            //         break;
            //
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
            person.Generate();

            var equipmentPoints = new EquipmentPoints(playerView.TransformModel.gameObject, _characterData);
            equipmentPoints.GenerateAllPoints();
            playerView.EquipmentPoints = equipmentPoints;

            var equipWeapon = new EquipmentWeapon(playerView, settings.Equipment);
            equipWeapon.GetWeapons();
            playerView.AnimatorParameters.WeaponType = equipWeapon.GetWeaponType();

            playerView.Transform.gameObject.layer = LayerManager.PlayerLayer;
        }
    }
}