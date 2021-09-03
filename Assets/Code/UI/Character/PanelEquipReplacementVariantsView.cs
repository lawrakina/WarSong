using System;
using Code.Extension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace Code.UI.Character
{
    public sealed class PanelEquipReplacementVariantsView : UiWindow
    {
        [SerializeField] private Button _close;
        [SerializeField] private Button _equipUnequipButton;
        [SerializeField] private GameObject _grid;
        [SerializeField] private GameObject _equpmentItem;
        [SerializeField] private Text _selectTitleItem;
        [SerializeField] private Text _selectDescriptionItem;
        

        public void Init(ListOfCellsReplacementVariants listItems, Action<SlotEquipment> putonOrTakeoffItem,
            UnityAction closeView)
        {
            _close.onClick.AddListener(closeView);
            _equipUnequipButton.onClick.AddListener(() => putonOrTakeoffItem(listItems.ActiveSlot));
            if (listItems.ActiveSlot.EquipmentItem)
            {
                var equipmentItem = Object.Instantiate(listItems.CellTemplate, _equpmentItem.transform, false);
                equipmentItem.Init(listItems.ActiveSlot);
            }
            foreach (var item in listItems)
            {
                var slot = Object.Instantiate(listItems.SlotHandler, _grid.transform, false);
                var cell = Object.Instantiate(listItems.CellTemplate, slot.transform, false);
                cell.Init(item);
            }
        }

        public void Clear()
        {
            _close.onClick.RemoveAllListeners();
            _equipUnequipButton.onClick.RemoveAllListeners();
            _grid.DestroyAllChildren();
            _equpmentItem.DestroyAllChildren();
        }
    }
}