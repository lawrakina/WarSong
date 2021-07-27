using Code.Unit;


namespace Code.Data.Unit.Player
{
    public class InfoAboutCharacter
    {
        private readonly IPlayerView _unit;

        public InfoAboutCharacter(IPlayerView unit)
        {
            _unit = unit;
        }

        public string FullInfo
        {
            get
            {
                if (_unit == null)
                {
                    return $"---";
                }

                var result = "";
                if (_unit.CharacterClass != null)
                    result += $"Class:{_unit.CharacterClass.Class}\n";
                if (_unit.UnitLevel != null)
                    result += $"Level:{_unit.UnitLevel.CurrentLevel}\n";
                if (_unit.UnitResource != null)
                    result += $"Resource:{_unit.UnitResource.ResourceType}";

                return result;
            }
        }

        public string AllCharacteristics =>
            $"Level: {_unit.UnitLevel.CurrentLevel}\n" +
            $"Equip level: {_unit.UnitEquipment.GetEquipmentItemsLevel}\n" +
            $"Attack: {_unit.UnitCharacteristics.MinAttack}-{_unit.UnitCharacteristics.MaxAttack}\n" +
            $"Armor: {_unit.UnitEquipment.FullArmor}({_unit.UnitCharacteristics.ArmorValue}%)\n" +
            $"HP: {_unit.UnitHealth.MaxHp}\n" +
            $"{_unit.UnitResource.ResourceType}: {_unit.UnitResource.MaxValue}\n" +
            $"Crit: {_unit.UnitCharacteristics.CritChance * 100f}%\n" +
            $"Dodge: {_unit.UnitCharacteristics.DodgeChance * 100f}%";
    }
}