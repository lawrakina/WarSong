using Code.Data;
using Code.Data.Dungeon;
using Code.Extension;
using Code.GameCamera;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit;
using UnityEngine;


namespace Code.Fight
{
    public sealed class EcsBattleController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private EcsBattle.EcsBattle _ecsBattle;
        private FightCamera _camera;
        private IPlayerView _player;
        private DungeonParams _dungeonParams;
        private InOutControlFightModel _inOutControlFightModel;

        public EcsBattleController(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _ecsBattle = Object.Instantiate(_profilePlayer.Settings.EcsBattleData.EcsBattle);
            _ecsBattle.gameObject.name = StringManager.ECS_BATTLE_GO_NAME;
        }

        public void StartFight()
        {
            OnExecute();
            
            _ecsBattle.Inject(_camera);
            _ecsBattle.Inject(_inOutControlFightModel);
            _ecsBattle.Inject(_dungeonParams);
            _ecsBattle.Inject(_player);
            
            // _listEnemies =
            // _enemiesInitialization.GetListEnemies(_generatorDungeon.GetEnemiesMarkers(),
            // _generatorDungeon.Dungeon());
            // _ecsBattle.Inject(_listEnemies);

            // _goal = _interactiveObjectsInitialization.GetGoal(_generatorDungeon.GetGoalLevelMarker());
            // _ecsBattle.Inject(_goal);
            
            _ecsBattle.Init(_profilePlayer);
        }

        public void UnSaveStopBattle()
        {
            // foreach (var enemy in _listEnemies)
            // {
            // Object.Destroy(enemy.Transform.gameObject);
            // }
            // _player.Transform.SetParent(GlobalLinks.Root);
            // _generatorDungeon.DestroyDungeon();
            _ecsBattle.DisposeWorld();
        }
        
        public void Inject(object obj)
        {
            if (obj is FightCamera camera)
                _camera = camera;
            if (obj is IPlayerView view)
                _player = view;
            if (obj is DungeonParams dunParams)
                _dungeonParams = dunParams;
            if (obj is InOutControlFightModel inputModel)
                _inOutControlFightModel = inputModel;
        }
    }
}