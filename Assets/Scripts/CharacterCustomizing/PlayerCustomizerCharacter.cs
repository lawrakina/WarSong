using Data;
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
            playerView.UnitVision = settings.unitVisionParameters;
            
            playerView.Attributes = new UnitAttributes();
            playerView.Attributes.Speed = settings.PlayerMoveSpeed;
            playerView.Attributes.RotateSpeedPlayer = settings.RotateSpeedPlayer;

            var person = new PersonCharacter(playerView.TransformModel.gameObject, _characterData);
            person.CharacterRace = settings.CharacterRace;
            person.CharacterGender = settings.CharacterGender;
            person.Generate();

            var equipmentPoints = new EquipmentPoints(playerView.TransformModel.gameObject, _characterData);
            equipmentPoints.GenerateAllPoints();
            playerView.EquipmentPoints = equipmentPoints;

            var equipWeapon = new EquipmentWeapon(playerView, settings.Equipment);
            equipWeapon.GetWeapons();
            playerView.AnimatorParameters.WeaponType = equipWeapon.GetWeaponType();

            playerView.Transform.gameObject.layer = LayerManager.PlayerLayer;
            playerView.UnitReputation = new UnitReputation();
            playerView.UnitReputation.FriendLayer = LayerManager.PlayerLayer;
            playerView.UnitReputation.EnemyLayer = LayerManager.EnemyLayer;
            playerView.UnitReputation.FriendAttackLayer = LayerManager.PlayerAttackLayer;
            playerView.UnitReputation.EnemyAttackLayer = LayerManager.EnemyAttackLayer;
        }
    }
}