using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class NeckEquip : BaseArmorItem, INeckEquip
    {
        public override int SubItemType => (int)ArmorItemType.Neck;
    }
}