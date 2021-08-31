using System.Linq;
using Code.Data.Dungeon;
using Code.Extension;
using Code.Profile;
using UnityEngine;


namespace Code.UI.Adventure
{
    public sealed class AdventureController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private AdventureView _view;

        public AdventureController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer): base(activate)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.AdventureView, _placeForUi,false);
            AddGameObjects(_view.gameObject);
            _view.Init(StartBattle);
            
            Init(activate);
        }

        private void StartBattle()
        {
            //ToDo сейчас тут заглушка на генерацию демоуровня, сделать полноценный выбор настроек уровня (выбор из БД варианта и присвоение его в актуальный вариант)
            _profilePlayer.Models.DungeonGeneratorModel.ActiveLevel = 
                _profilePlayer.Settings.DungeonGeneratorData.BdLevels
                    .FirstOrDefault(x => x.Type == DungeonParamsType.Demo);
            _profilePlayer.CurrentState.Value = GameState.Fight;
        }
    }
}