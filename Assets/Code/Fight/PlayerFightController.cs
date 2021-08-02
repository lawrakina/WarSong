using Code.Profile;
using Code.Profile.Models;
using UnityEngine;


namespace Code.Fight
{
    public sealed class PlayerFightController: BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightDungeonModel _model;

        public PlayerFightController(ProfilePlayer profilePlayer, FightDungeonModel model)
        {
            _profilePlayer = profilePlayer;
            _model = model;
            _model.OnChangePlayerPosition += SpawnPlayer;
        }

        private void SpawnPlayer(Transform spawnTransform)
        {
            _profilePlayer.CurrentPlayer.Transform.SetParent(spawnTransform);
            _profilePlayer.CurrentPlayer.Transform.localPosition = Vector3.zero;
        }

        protected override void OnDispose()
        {
            _model.OnChangePlayerPosition -= SpawnPlayer;
            base.OnDispose();
        }
    }
}