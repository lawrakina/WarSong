using CharacterCustomizing;
using Controller;
using Data;
using Extension;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private readonly CharacterData _characterData;
        private readonly PlayerCustomizerCharacter _playerCustomizerCharacter;
        private readonly UnitLevelInitialization _unitLevelInitialization;
        private readonly PlayerClassesInitialization _playerClassesInitialization;

        public PlayerFactory(PlayerCustomizerCharacter playerCustomizerCharacter, 
            UnitLevelInitialization unitLevelInitialization,
            PlayerClassesInitialization playerClassesInitialization,
            CharacterData characterData)
        {
            _unitLevelInitialization = unitLevelInitialization;
            _playerCustomizerCharacter = playerCustomizerCharacter;
            _playerClassesInitialization = playerClassesInitialization;
            _characterData = characterData;
        }

        public IPlayerView CreatePlayer(CharacterSettings item)
        {
            var player = Object.Instantiate(_characterData.StoragePlayerPrefab);
            player.name = $"PlayerCharacter.{item.CharacterClass}";
            player.AddCapsuleCollider(0.5f, false,
                      new Vector3(0.0f, 0.9f, 0.0f),
                      1.8f)
                  .AddRigidBody(80, CollisionDetectionMode.ContinuousSpeculative,
                      false, true,
                      RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                      RigidbodyConstraints.FreezeRotationZ)
                  .AddCode<PlayerView>();

            var playerView = player.GetComponent<IPlayerView>();
            _playerCustomizerCharacter.Customize(playerView, item);
            _unitLevelInitialization.Initialization(playerView, item);
            _playerClassesInitialization.Initialization(playerView,item);

            return playerView;
        }
    }
}