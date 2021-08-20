using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class ShoulderEquip : BaseArmorItem, IShoulderEquip
    {
        public override ArmorItemType ArmorItemType => ArmorItemType.Shoulder;
    }
}