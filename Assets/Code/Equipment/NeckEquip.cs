using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class NeckEquip : BaseArmorItem, INeckEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Neck;
    }
}