using System.Collections.Generic;
using Code.Data.Unit;
using Code.Extension;
using Code.Profile;
using UniRx;
using UnityEngine;

namespace Code.UI.Character
{
    internal class CharEditRootController : BaseController
    {
        private Transform _placeForUi;
        private ProfilePlayer _profilePlayer;
        private CharEditRootView _view;
        private List<EquipCellCommand> _equipmentCommands = new List<EquipCellCommand>();
        private CharEditReplacementEquipController _equipReplaceController;

        public CharEditRootController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer) : base(activate)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.CharEditRootView, _placeForUi, false);
            AddGameObjects(_view.GameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.AllCharacteristics).AddTo(_subscriptions);
            
            FillInListOfEquipment();

            _equipReplaceController = new CharEditReplacementEquipController(false, _placeForUi, _profilePlayer);
            AddController(_equipReplaceController, true);
            AddController(this, true, true);
            _profilePlayer.OnCharacterBuildIsComplete += ReloadData;
            Init(activate);
        }
        
        private void ReloadData()
        {
            _equipmentCommands.Clear();
            _equipmentCommands = new ListSelectableItems<EquipCellCommand>();
            _view.Clear();

            FillInListOfEquipment();
        }

        private void FillInListOfEquipment()
        {
            foreach (var equipCell in _profilePlayer.CurrentPlayer.UnitEquipment.Cells)
            {
                var cellCommand = new EquipCellCommand(equipCell);
                cellCommand.Command.Subscribe(CellClickHandler).AddTo(_subscriptions);
                _equipmentCommands.Add(cellCommand);
            }
            _view.Init(_equipmentCommands);
        }

        private void CellClickHandler(EquipCell item)
        {
            _equipReplaceController.Show(item);
        }

        public override void Dispose()
        {
            _profilePlayer.OnCharacterBuildIsComplete -= ReloadData;
            // Activate -= OnActivate;
            base.Dispose();
        }
    }
}