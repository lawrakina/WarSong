using System.Collections.Generic;
using Code.Data.Unit;
using Code.Equipment;
using Code.Extension;
using Code.Profile;
using UniRx;
using UnityEngine;


namespace Code.UI.Character
{
    public class EquipReplacementController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private ListOfCellsReplacementVariants _listItems;
        private PanelEquipReplacementVariantsView _view;
        private UnitInventory _inventory;
        private UnitEquipment _equipment;

        public EquipReplacementController(
            bool activate, Transform placeForUi, ProfilePlayer profilePlayer) : base(activate)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _inventory = _profilePlayer.CurrentPlayer.UnitInventory;
            _equipment = _profilePlayer.CurrentPlayer.UnitEquipment;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.PanelEquipReplacementVariantsView,
                _placeForUi, false);
            AddGameObjects(_view.GameObject);
            
            Init(activate);
        }

        public void Show(SlotEquipment value)
        {
            OnActivate();
            _listItems = new ListOfCellsReplacementVariants(
                _inventory.List, _profilePlayer.Settings.UiViews.CellTemplateDragAndDropEquipment,
                _profilePlayer.Settings.UiViews.SlotDropHandler, value);

            if (value.EquipmentItem)
            {
                var sourceItem = new SlotEquipment(value.EquipmentItem, value.SubItemType);
                sourceItem.Command.Subscribe(ShowInfoAboutSelectedItem).AddTo(_subscriptions);
                _listItems.ActiveSlot = sourceItem;
            }
            foreach (var item in _listItems.GetListByType)
            {
                var cell = new SlotEquipment(item, value.SubItemType);
                cell.Command.Subscribe(ShowInfoAboutSelectedItem).AddTo(_subscriptions);
                _listItems.Add(cell);
            }

            _view.Init(_listItems, PutonOrTakeoffItem, CloseView);
        }

        private void CloseView()
        {
            _listItems.Clear();
            _view.Clear();
            OnDeactivate();
        }

        private void ShowInfoAboutSelectedItem(SlotEquipment value)
        {
            Dbg.Log($"Selected info: {value.EquipmentItem}");
            _listItems.ActiveSlot = value;
        }

        private void PutonOrTakeoffItem(SlotEquipment value)
        {
            if (value == null) return;
            
            var item = _equipment.TakeOff(value.EquipmentItem);
            _inventory.Put(item);
            if (value != _listItems.ActiveSlot)
                _equipment.PutOn(value.EquipmentItem);
            
            _profilePlayer.RebuildCurrentCharacter.Invoke();
        }
    }
}