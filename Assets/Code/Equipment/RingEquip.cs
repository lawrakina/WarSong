using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class RingEquip : BaseArmorItem, IRingEquip
    {
        public override int SubItemType => (int)ArmorItemType.Ring;
    }
}