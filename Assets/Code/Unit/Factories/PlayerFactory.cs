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

            var rootPlayer = new GameObject {name = $"-> PlayerCharacter <-"};
            var player = rootPlayer.AddCode<PlayerView>();
            player.Transform = rootPlayer.transform;
            player.Rigidbody = rootPlayer.AddRigidBody(80, CollisionDetectionMode.ContinuousDynamic, false, true,
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ);
            player.Collider = rootPlayer.AddCapsuleCollider(0.5f, false,
                new Vector3(0.0f, 0.9f, 0.0f),
                1.8f);

            return player;
        }

        public IPlayerView RebuildModel(IPlayerView character, CharacterSettings settings,
            RaceCharacteristics raceCharacteristics)
        {
            if (character.TransformModel)
            {
                var trash = character.TransformModel.gameObject;
                trash.name = $"TRASH, Need destroy";
                Object.Destroy(trash);
            }

            var playerPrefab = Object.Instantiate(_data.StoragePlayerPrefab, character.Transform, true);
            playerPrefab.name = $"Prefab.Model";

            character.TransformModel = playerPrefab.transform;
            character.MeshRenderer = playerPrefab.GetComponent<MeshRenderer>();
            character.Animator = playerPrefab.GetComponent<Animator>();
            character.AnimatorParameters = new AnimatorParameters(character.Animator);
            character.UnitPerson = new UnitPerson(playerPrefab, settings, _data, raceCharacteristics);
            return character;
        }
    }
}