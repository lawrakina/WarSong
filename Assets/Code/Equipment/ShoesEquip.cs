using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class ShoesEquip : BaseArmorItem, IShoesEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Shoes;
    }
}