using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class BeltEquip : BaseArmorItem, IBeltEquip
    {
        public override int SubItemType => (int)ArmorItemType.Belt;
    }
}