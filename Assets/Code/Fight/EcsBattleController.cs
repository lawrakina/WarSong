using System.Collections.Generic;
using Code.Data;
using Code.Data.Dungeon;
using Code.Extension;
using Code.GameCamera;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit;
using ThirdPersonCameraWithLockOn;
using UnityEngine;


namespace Code.Fight
{
    public sealed class EcsBattleController : BaseController
    {
        private readonly Controllers _controllers;
        private readonly ProfilePlayer _profilePlayer;
        private EcsBattle.EcsBattle _ecsBattle;
        private ThirdPersonCamera _camera;
        private IPlayerView _player;
        private DungeonParams _dungeonParams;
        private EnemiesLevelModel _listEnemiesLevelModel;
        private InOutControlFightModel _inOutControlFightModel;

        public EcsBattleController(Controllers controllers, ProfilePlayer profilePlayer)
        {
            _controllers = controllers;
            _profilePlayer = profilePlayer;
            _ecsBattle = new EcsBattle.EcsBattle();
            // _ecsBattle = Object.Instantiate(_profilePlayer.Settings.EcsBattleData.EcsBattle);
            // _ecsBattle.gameObject.name = StringManager.ECS_BATTLE_GO_NAME;
            
            _controllers.Add(_ecsBattle);
            AddController(_ecsBattle);
        }

        public void StartFight()
        {
            OnExecute();
            
            _ecsBattle.Inject(_camera);
            _ecsBattle.Inject(_inOutControlFightModel);
            _ecsBattle.Inject(_dungeonParams);
            _ecsBattle.Inject(_listEnemiesLevelModel);
            _ecsBattle.Inject(_player);
            
            Dbg.Log($"1MODEL :{_listEnemiesLevelModel}");
            Dbg.Log($"1LISTLIST :{_listEnemiesLevelModel.Enemies.Count}");
            
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
            if (obj is ThirdPersonCamera camera)
                _camera = camera;
            if (obj is IPlayerView view)
                _player = view;
            if (obj is DungeonParams dunParams)
                _dungeonParams = dunParams;
            if (obj is InOutControlFightModel inputModel)
                _inOutControlFightModel = inputModel;
            if (obj is EnemiesLevelModel listEnemies)
                _listEnemiesLevelModel = listEnemies;
        }
    }
}