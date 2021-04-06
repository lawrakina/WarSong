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
        private readonly PlayerLevelInitialization _playerLevelInitialization;
        private readonly PlayerClassesInitialization _playerClassesInitialization;

        public PlayerFactory(CharacterData characterData,
            PlayerCustomizerCharacter playerCustomizerCharacter,
            PlayerLevelInitialization playerLevelInitialization,
            PlayerClassesInitialization playerClassesInitialization)
        {
            _playerLevelInitialization = playerLevelInitialization;
            _playerCustomizerCharacter = playerCustomizerCharacter;
            _playerClassesInitialization = playerClassesInitialization;
            _characterData = characterData;
        }

        public IPlayerView CreatePlayer(CharacterSettings item)
        {
            var playerPrefab = Object.Instantiate(_characterData.StoragePlayerPrefab);
                playerPrefab.name = $"Prefab.Model";
                
            var rootPlayer = Object.Instantiate(new GameObject());
                rootPlayer.name = $"PlayerCharacter.{item.CharacterClass}";
                
            var player = rootPlayer.AddCode<PlayerView>();
            // player.SetTransform(rootPlayer.transform);//root transform
            player.TransformModel = playerPrefab.transform;//prefab transform
            player.Rigidbody = rootPlayer.AddRigidBody(80, CollisionDetectionMode.ContinuousDynamic,
                false, true,
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ);
            player.Collider = rootPlayer.AddCapsuleCollider(0.5f, false,
                new Vector3(0.0f, 0.9f, 0.0f),
                1.8f);
            player.MeshRenderer = playerPrefab.GetComponent<MeshRenderer>();
            player.Animator = playerPrefab.GetComponent<Animator>();
            player.AnimatorParameters = new AnimatorParameters(player.Animator);
            playerPrefab.transform.SetParent(rootPlayer.transform);
                     
            _playerCustomizerCharacter.Customize(player, item);
            _playerLevelInitialization.Initialization(player, item);
            _playerClassesInitialization.Initialization(player, item);

            return player;
        }
    }
}