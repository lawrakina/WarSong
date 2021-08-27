using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class ShieldEquip : BaseArmorItem, IShieldEquip
    {
        public override int SubItemType => (int)ArmorItemType.Shield;
    }
}