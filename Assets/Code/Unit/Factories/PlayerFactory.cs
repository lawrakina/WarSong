using Code.Data.Unit;
using Code.Extension;
using UnityEngine;


namespace Code.Unit.Factories
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private readonly CharacterData _data;
        private CharacterSettings _settings;

        public PlayerFactory(CharacterData data)
        {
            _data = data;
        }

        public IPlayerView CreatePlayer(CharacterSettings settings)
        {
            _settings = settings;
            var playerPrefab = Object.Instantiate(_data.StoragePlayerPrefab);
            playerPrefab.name = $"Prefab.Model";

            var rootPlayer = Object.Instantiate(new GameObject());
            rootPlayer.name = $"PlayerCharacter";

            var player = rootPlayer.AddCode<PlayerView>();
            player.Transform = rootPlayer.transform;
            player.TransformModel = playerPrefab.transform; //prefab transform
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

            player.UnitPerson = new UnitPerson(player.TransformModel.gameObject, _settings, _data);

            return player;
        }
    }
}