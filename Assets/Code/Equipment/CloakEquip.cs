using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class CloakEquip : BaseArmorItem, ICloakEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Cloak;
    }
}