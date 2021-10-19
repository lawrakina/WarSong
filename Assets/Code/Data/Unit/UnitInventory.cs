using System.Collections.Generic;
using Code.Equipment;


namespace Code.Data.Unit{
    public class UnitInventory{
        private readonly CharacterEquipment _equipment;
        private List<BaseEquipItem> _inventory;

        public List<BaseEquipItem> List => _inventory;

        public UnitInventory(CharacterEquipment equipment){
            _equipment = equipment;
            _inventory = _equipment.Inventory;
        }
    }
}