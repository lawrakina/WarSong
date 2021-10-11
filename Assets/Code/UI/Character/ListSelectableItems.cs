using System;
using System.Collections.Generic;

namespace Code.UI.Character
{
    public class ListSelectableItems<T> : List<T>
    {
        public T EquippedItem { get; set; }
        private T _selectedItem;
        public Action<T> OnSelectNewItem;

        public T SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                OnSelectNewItem?.Invoke(value);
                _selectedItem = value;
            }
        }
    }
}