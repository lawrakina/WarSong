using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Character
{
    public sealed class CharEditRootView : UiWindow
    {
        [SerializeField] private Text _info;
        [SerializeField] private List<CellEquipClickHandler> _cells;

        public string InfoFormatted
        {
            set => _info.text = value;
        }

        public void Init(List<EquipCellCommand> listOfEquip)
        {
            foreach (var equip in listOfEquip)
            {
                foreach (var targetEquipCell in equip.Body.TargetEquipCells)
                {
                    foreach (var cellEquipClickHandler in _cells)
                    {
                        if (cellEquipClickHandler.TargetTypesOfEquip.Any(x => x == targetEquipCell))
                        {
                            cellEquipClickHandler.Init(equip.Body, equip.Command);
                            break;
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            foreach (var cell in _cells)
            {
                cell.Clear();
            }
        }
    }
}