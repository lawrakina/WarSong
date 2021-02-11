using System;


namespace Data
{
    [Serializable]
    public sealed class CharacterEquipment
    {
        public EquipmentSlot MainWeapon;
        public EquipmentSlot SecondWeapon;
    }
}