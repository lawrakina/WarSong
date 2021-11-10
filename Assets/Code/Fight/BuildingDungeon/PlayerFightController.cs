using System;
using Code.Extension;
using Code.Profile;
using Code.Profile.Models;
using UnityEngine;


namespace Code.Fight.BuildingDungeon
{
    public sealed class PlayerFightController : BaseController, IVerifiable
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightDungeonModel _model;

        public BuildStatus Status { get; set; }
    
        public event Action<IVerifiable> Complete = verifiable => { verifiable.Status = BuildStatus.Complete;};

        public PlayerFightController(ProfilePlayer profilePlayer, FightDungeonModel model)
        {
            Status = BuildStatus.Passive;
            _profilePlayer = profilePlayer;
            _model = model;
            _model.OnChangePlayerPosition += SpawnPlayer;
        }

        private void SpawnPlayer(Transform spawnTransform)
        {
            Status = BuildStatus.Process;
            _profilePlayer.CurrentPlayer.Motor.SetPosition(spawnTransform.position, false);
            
            Complete?.Invoke(this);
        }

        public override void Dispose()
        {
            _model.OnChangePlayerPosition -= SpawnPlayer;
            base.Dispose();
        }
    }
}