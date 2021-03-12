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
            var playerPrefab = Object.Instantiate(_characterData.StoragePlayerPrefab);
                playerPrefab.name = $"Prefab.Model";
                
            var rootPlayer = Object.Instantiate(new GameObject());
                rootPlayer.name = $"PlayerCharacter.{item.CharacterClass}";
                
            var player = rootPlayer.AddCode<PlayerView>();
            player.Transform = rootPlayer.transform;//root transform
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
                     
            // player.Animator = item.Animator;
            
            // private void Awake()
            // {
            //     Transform = GetComponent<Transform>();
            //     Rigidbody = GetComponent<Rigidbody>();
            //     Collider = GetComponent<Collider>();
            //     MeshRenderer = GetComponent<MeshRenderer>();
            //     _animator = GetComponent<Animator>();
            //     AnimatorParameters = new AnimatorParameters(ref _animator);
            // }

            // var playerView = playerPrefab.GetComponent<IPlayerView>();
            _playerCustomizerCharacter.Customize(player, item);
            _unitLevelInitialization.Initialization(player, item);
            _playerClassesInitialization.Initialization(player, item);

            return player;
        }
    }
}