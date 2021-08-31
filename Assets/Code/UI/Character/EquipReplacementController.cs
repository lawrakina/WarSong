using System.Collections.Generic;
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
        private PanelEquipReplacementVariantsView _view;
        private List<BaseEquipItem> _inventory;
        private ListOfCellsReplacementVariants _listItems;

        public EquipReplacementController(
            bool activate, Transform placeForUi, ProfilePlayer profilePlayer) : base(activate)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _inventory = _profilePlayer.Settings.PlayerData.ActiveCharacter.Equipment.Inventory;

            _view = ResourceLoader.InstantiateObject(_profilePlayer.Settings.UiViews.PanelEquipReplacementVariantsView,
                _placeForUi, false);
            AddGameObjects(_view.GameObject);
            
            Init(activate);
        }

        public void Show(CellEquipment value)
        {
            OnActivate();
            _listItems = new ListOfCellsReplacementVariants(
                _inventory, _profilePlayer.Settings.UiViews.Equipment_ClearCell, value);

            if (value.EquipmentItem)
            {
                var sourceItem = new CellEquipment(value.EquipmentItem, value.SubItemType);
                sourceItem.Command.Subscribe(ShowInfoAboutSelectedItem).AddTo(_subscriptions);
                _listItems.Add(sourceItem);
            }
            foreach (var item in _listItems.GetListByType)
            {
                var sell = new CellEquipment(item, value.SubItemType);
                sell.Command.Subscribe(ShowInfoAboutSelectedItem).AddTo(_subscriptions);
                _listItems.Add(sell);
            }

            _view.Init(_listItems, PutonOrTakeoffItem, CloseView);
        }

        private void CloseView()
        {
            _listItems.Clear();
            _view.Clear();
            OnDeactivate();
            // Dispose();
        }

        private void ShowInfoAboutSelectedItem(CellEquipment value)
        {
            Dbg.Log($"Selected info: {value.EquipmentItem}");
            // сделать выбранный элемент - value
            // показать инфу о выбранном итеме
        }

        private void PutonOrTakeoffItem(CellEquipment value)
        {
            Dbg.Log($"PutonOrTakeoffItem.{value.EquipmentItem}");
            // снять или одеть вещь (отправить в инвентарь или достать из него и отправить в UnitEquipment)
        }
    }
}