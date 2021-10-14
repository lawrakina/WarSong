using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Abilities;
using Code.Extension;
using Code.Profile;
using Code.UI.DragAndDrop;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.UI.Character.Abilities{
    public sealed class CharEditReplacementAbilitiesController : BaseController{
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private CharEditReplacementAbilitiesView _view;
        private ListOfAbilities _listOfAbilities;
        private List<AbilityCell> _activeAbilitiesFromDb;
        private List<AbilityCell> _allAbilitiesFromDb;
        private SelectableAbilityCell _cellTemplate;

        public CharEditReplacementAbilitiesController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer)
            : base(activate){
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _activeAbilitiesFromDb = _profilePlayer.CurrentPlayer.UnitAbilities.ActiveAbilities;
            _allAbilitiesFromDb = _profilePlayer.CurrentPlayer.UnitAbilities.AllAbilities;
            _cellTemplate = _profilePlayer.Settings.UiViews.AbilityTemplateCellClickAndSelectHandler;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.CharEditReplacementAbilitiesView,
                _placeForUi, false);
            AddGameObjects(_view.GameObject);

            _listOfAbilities = new ListOfAbilities();
            _listOfAbilities.OnSelectItem += OnObjectSelection;

            _profilePlayer.OnCharacterBuildIsComplete += Reload;

            _view.Init(ShowDetailsFromSelectedAbility, ImproveSelectedAbility, CloseView, HasChanged);

            Init(activate);
        }

        private void Reload(){
            _listOfAbilities.SelectedItem = null;
            Load();
        }

        private void HasChanged(Transform sender, Transform dropped){
            var newElement = dropped.GetComponent<SelectableAbilityCell>();
            var cellSender = sender.GetComponent<SelectableAbilityCell>();
            _profilePlayer.CurrentPlayer.UnitAbilities.ReplaceActiveAbility(newAbility: newElement.Body.Body, cellSender.CellType);
        }

        private void Load(){
            _listOfAbilities.Clear();
            _listOfAbilities = new ListOfAbilities();
            _view.Clear();

            var specialCell = Object.Instantiate(_cellTemplate, _view.SelectedAbilities, false)
                .GetComponent<SelectableAbilityCell>();
            specialCell.Init(_activeAbilitiesFromDb.FirstOrDefault(x =>
                x.AbilityCellType == AbilityCellType.Special), AbilityCellType.Special, OnObjectSelection);
            _listOfAbilities.SelectedList.Add(specialCell);

            var actionCell1 = Object.Instantiate(_cellTemplate, _view.SelectedAbilities, false)
                .GetComponent<SelectableAbilityCell>();
            actionCell1.Init(_activeAbilitiesFromDb.FirstOrDefault(x =>
                x.AbilityCellType == AbilityCellType.Action1), AbilityCellType.Action1, OnObjectSelection);
            _listOfAbilities.SelectedList.Add(actionCell1);

            var actionCell2 = Object.Instantiate(_cellTemplate, _view.SelectedAbilities, false)
                .GetComponent<SelectableAbilityCell>();
            actionCell2.Init(_activeAbilitiesFromDb.FirstOrDefault(x =>
                x.AbilityCellType == AbilityCellType.Action2), AbilityCellType.Action2, OnObjectSelection);
            _listOfAbilities.SelectedList.Add(actionCell2);

            var actionCell3 = Object.Instantiate(_cellTemplate, _view.SelectedAbilities, false)
                .GetComponent<SelectableAbilityCell>();
            actionCell3.Init(_activeAbilitiesFromDb.FirstOrDefault(x =>
                x.AbilityCellType == AbilityCellType.Action3), AbilityCellType.Action3, OnObjectSelection);
            _listOfAbilities.SelectedList.Add(actionCell3);

            //add Drag and Drop handler
            specialCell.gameObject.AddCode<ReplaceSlotHandler>();
            actionCell1.gameObject.AddCode<ReplaceSlotHandler>();
            actionCell2.gameObject.AddCode<ReplaceSlotHandler>();
            actionCell3.gameObject.AddCode<ReplaceSlotHandler>();
            
            foreach (var cell in _allAbilitiesFromDb){
                var cellCommand = Object.Instantiate(_cellTemplate, _view.ListOfAbilities, false)
                    .GetComponent<SelectableAbilityCell>();
                cellCommand.Init(cell, AbilityCellType.IsStock, OnObjectSelection);
                _listOfAbilities.SelectedList.Add(cellCommand);
                cellCommand.gameObject.AddCode<DragHandler>();
                cellCommand.gameObject.AddCode<CanvasGroup>();
            }

        }

        private void OnObjectSelection(SelectableAbilityCell obj){
            if (obj == null) return;
            if(_listOfAbilities.SelectedItem != null)
                _listOfAbilities.SelectedItem.IsSelect = false;
            _listOfAbilities.SelectedItem = obj;
            obj.IsSelect = true;
            UpdateInfo();
        }

        private void UpdateInfo(){
            if (_listOfAbilities.SelectedItem){
                _view.TitleSelectedAbility = _listOfAbilities.SelectedItem.Body.Body.uiInfo.Title;
                _view.DescriptionSelectedAbility = _listOfAbilities.SelectedItem.Body.Body.uiInfo.Description;
                _view.IconSelectedAbility = _listOfAbilities.SelectedItem.Body.Body.uiInfo.Icon;
                _view.ButtonImpove.interactable = true;
                return;
            }

            _view.TitleSelectedAbility = _listOfAbilities.SelectedItem.Body.Body.uiInfo.Title;
            _view.DescriptionSelectedAbility = _listOfAbilities.SelectedItem.Body.Body.uiInfo.Description;
            _view.IconSelectedAbility = _listOfAbilities.SelectedItem.Body.Body.uiInfo.Icon;
            _view.ButtonImpove.interactable = false;
        }

        public void Show(AbilityCell selectedCell){
            OnActivate();
            Reload();
            foreach (var ability in _listOfAbilities){
                if (ability.CellType == selectedCell.AbilityCellType)
                    ability.IsSelect = true;
            }
        }

        private void ShowDetailsFromSelectedAbility(){
            Dbg.Warning($"CharEditReplacementAbilitiesController.ShowDetailsFromSelectedAbility NOT IMPLEMENTED!!!");
        }

        private void ImproveSelectedAbility(){
            Dbg.Warning($"CharEditReplacementAbilitiesController.ImproveSelectedAbility NOT IMPLEMENTED!!!");
        }

        private void CloseView(){
            _view.Clear();
            OnDeactivate();
        }
    }
}