using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class BraceletEquip : BaseArmorItem, IBraceletEquip
    {
        public override int SubItemType => (int)ArmorItemType.Bracelet;
    }
}