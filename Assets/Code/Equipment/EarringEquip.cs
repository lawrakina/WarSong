using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class EarringEquip : BaseArmorItem, IEarringEquip
    {
        public override int SubItemType => (int)ArmorItemType.Earring;
    }
}