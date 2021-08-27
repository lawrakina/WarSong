using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class ShoulderEquip : BaseArmorItem, IShoulderEquip
    {
        public override int SubItemType => (int)ArmorItemType.Shoulder;
    }
}