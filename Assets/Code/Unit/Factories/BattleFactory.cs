using Code.Data.Unit;
using Code.Data.Unit.Player;


namespace Code.Unit.Factories{
    public sealed class BattleFactory{
        private readonly PlayerClassesData _settings;

        public BattleFactory(PlayerClassesData settings){
            _settings = settings;
        }

        public UnitBattle GenerateBattle(UnitCharacteristics characteristics, UnitEquipment equip){
            return new UnitBattle(characteristics, equip);
        }
    }
}