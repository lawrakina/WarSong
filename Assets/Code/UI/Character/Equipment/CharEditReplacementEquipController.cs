using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Equipment;
using Code.Extension;
using Code.Profile;
using Code.UI.UniversalTemplates;
using UnityEngine;


namespace Code.UI.Character.Equipment{
    public sealed class CharEditReplacementEquipController : BaseController{
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private CharEditReplacementEquipView _view;
        private SelectableEquipCell _cellTemplate;
        private ListSelectableItems<SelectableEquipCell> _listItems;
        private UnitEquipment _equipment;
        private UnitInventory _inventory;
        private EquipCellType _equipCellType;
        private List<TargetEquipCell> _targetEquipCells;

        public CharEditReplacementEquipController(bool activate, Transform placeForUi, ProfilePlayer profilePlayer) :
            base(activate){
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _equipment = _profilePlayer.CurrentPlayer.UnitEquipment;
            _inventory = _profilePlayer.CurrentPlayer.UnitInventory;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.CharEditReplacementEquipView,
                _placeForUi, false);
            AddGameObjects(_view.GameObject);
            _cellTemplate = _profilePlayer.Settings.UiViews.EquipmentTemplateCellClickAndSelectHandler;

            _listItems = new ListSelectableItems<SelectableEquipCell>();

            _listItems.OnSelectNewItem += OnObjectSelection;

            _profilePlayer.OnCharacterBuildIsComplete += Reload;

            _view.Init(PutOnOrTakeoffItem, CloseView);

            Init(activate);
        }

        private void Reload(){
            _listItems.ActiveItem = null;
            _listItems.SelectedItem = null;

            Load(_equipCellType, _targetEquipCells);
        }

        private void Load(EquipCellType equipCellType, List<TargetEquipCell> targetEquipCells){
            _listItems.Clear();
            _listItems = new ListSelectableItems<SelectableEquipCell>();
            _view.Clear();

            var listItemFromInventory = new List<BaseEquipItem>();

            //запрос предметов подходящих для данной ячейки
            foreach (var item in _inventory.List){
                foreach (var targetEquipCell in targetEquipCells){
                    if (item.TargetEquipCell == targetEquipCell){
                        listItemFromInventory.Add(item);
                    }
                }
            }

            //запрос надетой вещи
            var equipmentCell =
                _profilePlayer.CurrentPlayer.UnitEquipment.Cells.FirstOrDefault(x => x.EquipCellType == equipCellType);

            //есть надетая вещь? Выделяем маркером
            if (equipmentCell != null && !equipmentCell.IsEmpty){
                var equippedItem = Object.Instantiate(_cellTemplate, _view.Grid, false)
                    .GetComponent<SelectableEquipCell>();
                equippedItem.Init(equipmentCell.Body, equipCellType, OnObjectSelection, true);
                _listItems.ActiveItem = equippedItem;
            }

            //добавляем ячейки с подходящими вещами
            foreach (var baseItem in listItemFromInventory){
                var item = Object.Instantiate(_cellTemplate, _view.Grid, false)
                    .GetComponent<SelectableEquipCell>();
                item.Init(baseItem, equipCellType, OnObjectSelection);
                _listItems.Add(item);
            }

            UpdateInfo();
        }

        private void OnObjectSelection(SelectableEquipCell obj){
            Dbg.Log($"OnObjectSelection.{obj}");
            if (_listItems.SelectedItem == obj){
                _listItems.SelectedItem = null;
                obj.IsSelect = false;
            }
            else{
                if (_listItems.SelectedItem != null)
                    _listItems.SelectedItem.IsSelect = false;
                _listItems.SelectedItem = obj;
                obj.IsSelect = true;
            }

            UpdateInfo();
        }

        private void UpdateInfo(){
            _view.InfoItemLevel = $"Item Level: {_equipment.GetFullEquipmentItemsLevel}";
            if (_profilePlayer.InfoAboutCurrentPlayer.HasValue)
                _view.InfoSummaryByChar = _profilePlayer.InfoAboutCurrentPlayer.Value.AllCharacteristics;

            if (_listItems.SelectedItem){
                _view.InfoTitleSelectedItem = $"Select item: {_listItems.SelectedItem.Body.UiInfo.Title}";
                _view.InfoDescriptionSelectedItem = $"{_listItems.SelectedItem.Body.UiInfo.Description}";
                return;
            }

            if (_listItems.ActiveItem){
                _view.InfoTitleSelectedItem = $"Equipped item: {_listItems.ActiveItem.Body.UiInfo.Title}";
                _view.InfoDescriptionSelectedItem = $"{_listItems.ActiveItem.Body.UiInfo.Description}";
                return;
            }

            _view.InfoTitleSelectedItem = $"---";
            _view.InfoDescriptionSelectedItem = $"";
        }

        public void Show(EquipCell equipCell){
            OnActivate();
            _equipCellType = equipCell.EquipCellType;
            _targetEquipCells = equipCell.TargetEquipCells;
            Load(_equipCellType, _targetEquipCells);
        }

        private void PutOnOrTakeoffItem(){
            Dbg.Log($"PutOnOrTakeoffItem.SelectItem:{_listItems.SelectedItem}, EquippedItem:{_listItems.ActiveItem}");
            var equip = _listItems.ActiveItem;
            var select = _listItems.SelectedItem;

            if (select == null)
                return;

            //Takeoff Equipped item
            if (select == equip)
                _equipment.SentToInventory(equip.Body);
            //PutOn Selected item
            if (select != equip){
                if (equip != null)
                    if (equip.Body != null)
                        _equipment.SentToInventory(equip.Body);
                _equipment.SendToEquipment(select.Body, select.CellType);
            }

            _profilePlayer.RebuildCurrentCharacter?.Invoke();
        }

        private void CloseView(){
            _view.Clear();
            OnDeactivate();
        }
    }
}