using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class CloakEquip : BaseArmorItem, ICloakEquip
    {
        public override int SubItemType => (int)ArmorItemType.Cloak;
    }
}