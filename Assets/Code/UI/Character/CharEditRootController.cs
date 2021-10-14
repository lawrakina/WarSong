using System.Collections.Generic;
using Code.Data.Abilities;
using Code.Data.Unit;
using Code.Extension;
using Code.Profile;
using Code.UI.Character.Abilities;
using Code.UI.Character.Equipment;
using Code.UI.UniversalTemplates;
using UniRx;
using UnityEngine;


namespace Code.UI.Character{
    internal class CharEditRootController : BaseController{
        private Transform _placeForUi;
        private ProfilePlayer _profilePlayer;
        private CharEditRootView _view;
        private List<CellCommand<EquipCell>> _equipmentCommands = new List<CellCommand<EquipCell>>();
        private List<CellCommand<AbilityCell>> _abilitiesCommands = new List<CellCommand<AbilityCell>>();
        // private List<CellCommand<TalentCell>> _talentsCommands = new List<CellCommand<TalentCell>>();
        private CharEditReplacementEquipController _equipReplaceController;
        private CharEditReplacementAbilitiesController _abilitiesReplaceController;
        private CharEditReplacementTalentsController _talentsReplaceController;

        public CharEditRootController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer) :
            base(activate){
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _view = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.CharEditRootView, _placeForUi, false);
            AddGameObjects(_view.GameObject);
            _profilePlayer.InfoAboutCurrentPlayer
                .Subscribe(info => _view.InfoFormatted = info.AllCharacteristics).AddTo(_subscriptions);

            _profilePlayer.OnCharacterBuildIsComplete += ReloadData;
            FillInLists();

            _equipReplaceController = new CharEditReplacementEquipController(false, _placeForUi, _profilePlayer);
            AddController(_equipReplaceController, true);

            _abilitiesReplaceController =
                new CharEditReplacementAbilitiesController(false, _placeForUi, _profilePlayer);
            AddController(_abilitiesReplaceController, true);

            _talentsReplaceController = new CharEditReplacementTalentsController(false, _placeForUi, _profilePlayer);
            AddController(_talentsReplaceController, true);

            AddController(this, true, true);

            Init(activate);
        }

        private void ReloadData(){
            _equipmentCommands.Clear();
            _equipmentCommands = new ListSelectableItems<CellCommand<EquipCell>>();

            _abilitiesCommands.Clear();
            _abilitiesCommands = new ListSelectableItems<CellCommand<AbilityCell>>();

            // _talentsCommands.Clear();
            // _talentsCommands = new ListSelectableItems<CellCommand<TalentCell>>();
            _view.Clear();

            FillInLists();
        }

        // private void FillInListOfTalents(){
        // foreach (var abilityCell in _profilePlayer.CurrentPlayer.UnitTalents.ActiveCells){
        // var cellCommand = new CellCommand<TalentCell>(abilityCell);
        // cellCommand.Command.Subscribe(CellTalentsClickHandler).AddTo(_subscriptions);
        // _talentsCommands.Add(cellCommand);
        // }
        // }

        private void FillInListOfAbilities(){
            foreach (var abilityCell in _profilePlayer.CurrentPlayer.UnitAbilities.ActiveAbilities){
                Dbg.Log($"----------- Ability:{abilityCell.AbilityCellType},{abilityCell.Body.uiInfo.Title}");
                var cellCommand = new CellCommand<AbilityCell>(abilityCell);
                cellCommand.Command.Subscribe(CellAbilityClickHandler).AddTo(_subscriptions);
                _abilitiesCommands.Add(cellCommand);
            }
        }

        private void FillInListOfEquipment(){
            foreach (var equipCell in _profilePlayer.CurrentPlayer.UnitEquipment.Cells){
                var cellCommand = new CellCommand<EquipCell>(equipCell);
                cellCommand.Command.Subscribe(CellEquipClickHandler).AddTo(_subscriptions);
                _equipmentCommands.Add(cellCommand);
            }
        }

        private void FillInLists(){

            FillInListOfEquipment();
            FillInListOfAbilities();
            // FillInListOfTalents();
            _view.Init(_equipmentCommands, _abilitiesCommands, null /*_talentsCommands*/);
        }

        private void CellEquipClickHandler(EquipCell item){
            _equipReplaceController.Show(item);
        }

        private void CellAbilityClickHandler(AbilityCell item){
            Dbg.Log($"Click to ability cell:{item.AbilityCellType}, {item.Body.uiInfo.Title}");
            _abilitiesReplaceController.Show(item);
        }

        // private void CellTalentsClickHandler(TalentCell item){
        // _talentsReplaceController.Show(item);
        // }

        public override void Dispose(){
            _profilePlayer.OnCharacterBuildIsComplete -= ReloadData;
            // Activate -= OnActivate;
            base.Dispose();
        }
    }

    public sealed class CharEditReplacementTalentsController : BaseController{
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        public CharEditReplacementTalentsController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer) :
            base(activate){
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;


            Init(activate);
        }
    }
}