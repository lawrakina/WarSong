using Code.CharacterCustomizing;
using Code.Data.Unit;
using Code.Extension;
using UnityEngine;


namespace Code.Unit.Factories
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private readonly CharacterData _settings;

        public PlayerFactory(CharacterData settings)
        {
            _settings = settings;
        }

        public IPlayerView CreatePlayer(CharacterSettings settings)
        {
            var playerPrefab = Object.Instantiate(_settings.StoragePlayerPrefab);
            playerPrefab.name = $"Prefab.Model";
                
            var rootPlayer = Object.Instantiate(new GameObject());
            rootPlayer.name = $"PlayerCharacter";
                
            var player = rootPlayer.AddCode<PlayerView>();
            player.Transform = rootPlayer.transform;
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
            
            var person = new PersonCharacter(player.TransformModel.gameObject, _settings);
            person.CharacterRace = settings.CharacterRace;
            person.CharacterGender = settings.CharacterGender;
            person.Generate();
            player.PersonCharacter = person;

            var equipmentPoints = new EquipmentPoints(player.TransformModel.gameObject, _settings);
            equipmentPoints.GenerateAllPoints();

            player.UnitEquipment = new UnitEquipment(equipmentPoints, settings.Equipment);
            
            // player.CharacterClass = new BaseCharacterClass();
            
            // player.UnitLevel = new UnitLevel();
            // player.UnitCharacteristics = new UnitCharacteristics();
            
            return player;
        }
    }
}