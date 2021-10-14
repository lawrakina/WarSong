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
            _profilePlayer.CurrentPlayer.UnitAbilities.ReplaceActiveAbility(newElement.Body.Body, cellSender.CellType);

            _profilePlayer.RebuildCurrentCharacter?.Invoke();
        }

        private void Load(){
            if (!IsOn) return;
            _listOfAbilities.Clear();
            _listOfAbilities = new ListOfAbilities();
            _view.Clear();

            _listOfAbilities.SelectedList.Add(ConfigureAbitilyCell(AbilityCellType.Special));
            _listOfAbilities.SelectedList.Add(ConfigureAbitilyCell(AbilityCellType.Action1));
            _listOfAbilities.SelectedList.Add(ConfigureAbitilyCell(AbilityCellType.Action2));
            _listOfAbilities.SelectedList.Add(ConfigureAbitilyCell(AbilityCellType.Action3));

            foreach (var cell in _allAbilitiesFromDb){
                var cellCommand = Object.Instantiate(_cellTemplate, _view.ListOfAbilities, false)
                    .GetComponent<SelectableAbilityCell>();
                cellCommand.Init(cell, AbilityCellType.IsStock, OnObjectSelection);
                cellCommand.gameObject.AddCode<DragHandler>();
                cellCommand.gameObject.AddCode<CanvasGroup>();
            }
        }

        private SelectableAbilityCell ConfigureAbitilyCell(AbilityCellType cellType){
            var result = Object.Instantiate(_cellTemplate, _view.SelectedAbilities, false)
                .GetComponent<SelectableAbilityCell>();
            result.Init(_profilePlayer.CurrentPlayer.UnitAbilities.ActiveAbilities.FirstOrDefault(x =>
                x.AbilityCellType == cellType), cellType, OnObjectSelection);
            result.gameObject.AddCode<ReplaceSlotHandler>();
            return result;
        }

        private void OnObjectSelection(SelectableAbilityCell obj){
            if (obj == null) return;
            if (_listOfAbilities.SelectedItem != null)
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