using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class GlovesEquip : BaseArmorItem, IGlovesEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Gloves;
    }
}