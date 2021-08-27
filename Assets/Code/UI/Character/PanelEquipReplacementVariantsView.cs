using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.UI.Character
{
    public sealed class PanelEquipReplacementVariantsView : UiWindow
    {
        [SerializeField] private GameObject _grid;
        public void Init(ListOfCellsReplacementVariants listItems, Action<CellEquipment> putonOrTakeoffItem)
        {
            foreach (var item in listItems)
            {
                var cell = Object.Instantiate(listItems.CellTemplate, _grid.transform, false);
                cell.Init(item);
            }
        }
    }
}