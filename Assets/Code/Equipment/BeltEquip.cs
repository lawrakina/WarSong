using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class BeltEquip : BaseArmorItem, IBeltEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Belt;
    }
}