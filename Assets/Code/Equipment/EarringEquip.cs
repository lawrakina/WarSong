using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class EarringEquip : BaseArmorItem, IEarringEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Earring;
    }
}