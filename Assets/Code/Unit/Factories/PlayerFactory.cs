using Code.Data.Unit;
using Code.Extension;
using KinematicCharacterController;
using UnityEngine;


namespace Code.Unit.Factories{
    public sealed class PlayerFactory : IPlayerFactory{
        private readonly CharacterData _data;
        private CharacterSettings _settings;

        public PlayerFactory(CharacterData data){
            _data = data;
        }

        public IPlayerView CreatePlayer(CharacterSettings settings){
            _settings = settings;

            var rootPlayer = Object.Instantiate(_data.StorageRootPrefab);
            rootPlayer.name = $"-> PlayerCharacter <-";
            var player = rootPlayer.AddCode<PlayerView>();
            player.Transform = rootPlayer.transform;
            player.Motor = rootPlayer.GetComponent<KinematicCharacterMotor>();
            player.UnitMovement = rootPlayer.GetComponent<UnitMovement>();
            player.Collider = rootPlayer.GetComponent<CapsuleCollider>();

            return player;
        }

        public IPlayerView RebuildModel(IPlayerView player, CharacterSettings settings,
            RaceCharacteristics raceCharacteristics){
            if (player.TransformModel){
                var trash = player.TransformModel.gameObject;
                trash.name = $"TRASH, Need destroy";
                Object.Destroy(trash);
            }

            var playerPrefab = Object.Instantiate(_data.StorageModelPrefab, player.UnitMovement.MeshRoot, true);
            playerPrefab.name = $"Prefab.Model";

            player.TransformModel = playerPrefab.transform;
            player.MeshRenderer = playerPrefab.GetComponent<MeshRenderer>();
            player.Animator = playerPrefab.GetComponent<Animator>();
            player.Animator.enabled = true;
            player.AnimatorParameters = new AnimatorParameters(player.Animator);
            player.UnitPerson = new UnitPerson(playerPrefab, settings, _data, raceCharacteristics);
            return player;
        }
    }
}