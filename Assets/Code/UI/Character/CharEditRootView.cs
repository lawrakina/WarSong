using System.Collections.Generic;
using System.Linq;
using Code.Data.Abilities;
using Code.Data.Unit;
using Code.UI.Character.Abilities;
using Code.UI.Character.Equipment;
using Code.UI.UniversalTemplates;
using UnityEngine;
using UnityEngine.UI;


namespace Code.UI.Character{
    public sealed class CharEditRootView : UiWindow{
        [SerializeField]
        private Text _info;

        [SerializeField]
        private List<CellEquipClickHandler> _cells;

        [SerializeField]
        private List<CellAbilityClickHandler> _abilities;

        public string InfoFormatted{
            set => _info.text = value;
        }

        public void Clear(){
            foreach (var cell in _cells){
                cell.Clear();
            }

            foreach (var ability in _abilities){
                ability.Clear();
            }
        }

        public void Init(List<CellCommand<EquipCell>> listOfEquip, List<CellCommand<AbilityCell>> listOfAbilities, object test /*List<CellCommand<TalentCell>> listOfTalents*/){
            //Equipment
            foreach (var equip in listOfEquip){
                foreach (var targetEquipCell in equip.Body.TargetEquipCells){
                    foreach (var cellEquipClickHandler in _cells){
                        if (cellEquipClickHandler.TargetTypesOfEquip.Any(x => x == targetEquipCell)){
                            cellEquipClickHandler.Init(equip.Body, equip.Command);
                            break;
                        }
                    }
                }
            }
            //Abilities
            foreach (var ability in listOfAbilities){
                foreach (var cellAbilityClickHandler in _abilities){
                    if(cellAbilityClickHandler.TargetTypesOfAbility == ability.Body.AbilityCellType)
                        cellAbilityClickHandler.Init(ability.Body, ability.Command);
                }
            }
            //Talents
            ///////
        }
    }
}