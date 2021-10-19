using System;
using System.Collections.Generic;


namespace Code.UI.Character.Abilities{
    public class ListOfAbilities : List<SelectableAbilityCell>{
        private SelectableAbilityCell _selectedItem;
        private List<SelectableAbilityCell> _selectedList = new List<SelectableAbilityCell>();
        public Action<SelectableAbilityCell> OnSelectItem;
        public SelectableAbilityCell SelectedItem{
            get{ return _selectedItem; }
            set{
                OnSelectItem?.Invoke(value);
                _selectedItem = value;
            }
        }
        public List<SelectableAbilityCell> SelectedList => _selectedList;
    }
}