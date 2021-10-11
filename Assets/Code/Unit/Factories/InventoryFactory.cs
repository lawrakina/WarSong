using System.Collections.Generic;
using Code.Data.Unit;
using Code.Equipment;


namespace Code.Unit.Factories
{
    public sealed class InventoryFactory
    {
        private IPlayerView _character;
        private CharacterSettings _settings;
        private UnitInventory _inventory;

        public UnitInventory GenerateInventory(IPlayerView character, CharacterSettings settings)
        {
            _character = character;
            _settings = settings;
            _inventory = new UnitInventory(_settings.Equipment);
            return _inventory;
        }
    }

    public class UnitInventory
    {
        private readonly CharacterEquipment _equipment;
        private List<BaseEquipItem> _inventory;

        public List<BaseEquipItem> List => _inventory;
        
        public UnitInventory(CharacterEquipment equipment)
        {
            _equipment = equipment;
            _inventory = _equipment.Inventory;
        }

    }
}