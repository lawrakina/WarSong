using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class BodyEquip : BaseArmorItem, IBodyEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Body;
    }
}