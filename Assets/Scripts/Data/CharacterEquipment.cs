using System;
using Weapons;


namespace Data
{
    [Serializable]
    public sealed class CharacterEquipment
    {
        public BaseWeapon MainWeapon;
        public BaseWeapon SecondWeapon;
    }
}