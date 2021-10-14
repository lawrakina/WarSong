using System;
using UnityEngine;


namespace Code.Data.Abilities{
    [Serializable] public class KvpAbilityAndCellType{
        [SerializeField]
        private AbilityCellType _cellType;
        [SerializeField]
        private TemplateAbility _ability;
        public AbilityCellType CellType{
            get => _cellType;
            set => _cellType = value;
        }
        public TemplateAbility Ability{
            get => _ability;
            set => _ability = value;
        }

        public KvpAbilityAndCellType(TemplateAbility ability, AbilityCellType cellType){
            Ability = ability;
            CellType = cellType;
        }
    }
}