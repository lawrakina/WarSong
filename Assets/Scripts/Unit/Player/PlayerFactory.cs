using CharacterCustomizing;
using Code.Extension;
using Controller;
using Data;
using UnityEngine;


namespace Unit.Player
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private readonly CharacterData _characterData;
        private readonly PlayerCustomizerCharacter _playerCustomizerCharacter;
        private readonly PlayerLevelInitialization _playerLevelInitialization;
        private readonly PlayerClassesInitialization _playerClassesInitialization;
        // private readonly PlayerAbilitiesInitialization _playerAbilitiesInitialization;

        public PlayerFactory(CharacterData characterData,
            PlayerCustomizerCharacter playerCustomizerCharacter,
            PlayerLevelInitialization playerLevelInitialization,
            PlayerClassesInitialization playerClassesInitialization
            // PlayerAbilitiesInitialization playerAbilitiesInitialization
            )
        {
            _playerLevelInitialization = playerLevelInitialization;
            _playerCustomizerCharacter = playerCustomizerCharacter;
            _playerClassesInitialization = playerClassesInitialization;
            // _playerAbilitiesInitialization = playerAbilitiesInitialization;
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

            player.Modifier = new UnitModifier(player, item,
                _playerCustomizerCharacter,
                _playerLevelInitialization,
                _playerClassesInitialization
            );

            player.Modifier.Init();

            return player;
        }
    }

    public sealed class UnitModifier
    {
        private readonly IPlayerView _player;
        private readonly CharacterSettings _settings;
        private readonly PlayerCustomizerCharacter _playerCustomizerCharacter;
        private readonly PlayerLevelInitialization _playerLevelInitialization;
        private readonly PlayerClassesInitialization _playerClassesInitialization;

        public UnitModifier(IPlayerView player, CharacterSettings settings,
            PlayerCustomizerCharacter playerCustomizerCharacter, PlayerLevelInitialization playerLevelInitialization,
            PlayerClassesInitialization playerClassesInitialization)
        {
            _player = player;
            _settings = settings;
            _playerCustomizerCharacter = playerCustomizerCharacter;
            _playerLevelInitialization = playerLevelInitialization;
            _playerClassesInitialization = playerClassesInitialization;
        }

        public void Init()
        {
            _playerCustomizerCharacter.Customize(_player, _settings);
            _playerLevelInitialization.Initialization(_player, _settings);
            _playerClassesInitialization.Initialization(_player, _settings);
            // _playerAbilitiesInitialization.Initialization(player, item);
        }
    }
}