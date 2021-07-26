using Code.Extension;
using Code.Unit;


namespace Code.Data.Unit.Player
{
    public class InfoAboutCharacter
    {
        private readonly IPlayerView _unit;

        public InfoAboutCharacter(IPlayerView unit)
        {
            _unit = unit;
            Dbg.Log($"1111\n" +
                    $"{_unit}");
        }

        public string FullInfo
        {
            get
            {
                if (_unit == null)
                {
                    return $"---";
                }
                return
                    $"Class:{_unit.CharacterClass.Class}\n" +
                    $"Level:{_unit.UnitLevel.CurrentLevel}\n" +
                    $"Resource:{_unit.CharacterClass.ResourceType}";
            }
        }

        public string AllCharacteristics =>
            $"Level: {_unit.UnitLevel.CurrentLevel}\n" +
            $"Equip level: {_unit.UnitEquipment.GetEquipmentItemsLevel}\n" +
            // $"Attack: {_unit.UnitCharacteristics.MinAttack}-{_unit.UnitCharacteristics.MaxAttack}\n" +
            // $"Armor: {_unit.UnitEquipment.FullArmor}({_unit.UnitCharacteristics.ArmorValue}%)\n" +
            // $"HP: {_unit.UnitHealth.MaxHp}\n" +
            // $"{_unit.CharacterClass.ResourceType}: {_unit.UnitResource.MaxValue}\n" +
            $"Crit: {_unit.UnitCharacteristics.CritChance * 100f}%\n" +
            $"Dodge: {_unit.UnitCharacteristics.DodgeChance * 100f}%";
    }
}