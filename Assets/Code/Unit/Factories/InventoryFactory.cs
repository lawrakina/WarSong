using System.Collections;
using Code.Data.Unit;


namespace Code.Unit.Factories{
    public sealed class InventoryFactory{
        private IPlayerView _character;
        private CharacterSettings _settings;
        private UnitInventory _inventory;

        public UnitInventory GenerateInventory(IPlayerView character, CharacterSettings settings){
            _character = character;
            _settings = settings;
            _inventory = new UnitInventory(_settings.Equipment);
            return _inventory;
        }
    }
}