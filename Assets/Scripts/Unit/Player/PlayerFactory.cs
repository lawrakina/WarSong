using CharacterCustomizing;
using Controller;
using Data;
using Extension;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Unit.Player
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private CharacterData _characterData;
        private CustomizerCharacter _customizerCharacter;

        public PlayerFactory(CustomizerCharacter customizerCharacter, CharacterData characterData)
        {
            _customizerCharacter = customizerCharacter;
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
            _customizerCharacter.Customize(ref playerView, item);

            return playerView;
        }
    }
}