using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace Code.UI.Character
{
    public sealed class PanelEquipReplacementVariantsView : UiWindow
    {
        [SerializeField] private Button _close;
        [SerializeField] private GameObject _grid;
        public void Init(ListOfCellsReplacementVariants listItems, Action<CellEquipment> putonOrTakeoffItem, UnityAction closeView)
        {
            _close.onClick.AddListener(closeView);
            foreach (var item in listItems)
            {
                var cell = Object.Instantiate(listItems.CellTemplate, _grid.transform, false);
                cell.Init(item);
            }
        }
    }
}