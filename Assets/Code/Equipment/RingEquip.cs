using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class RingEquip : BaseArmorItem, IRingEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Ring;
    }
}