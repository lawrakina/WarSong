using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class PantsEquip : BaseArmorItem, IPantsEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Pants;
    }
}