using System;
using Code.Data;


namespace Code.Equipment
{
    [Serializable]
    public class GlovesEquip : BaseArmorItem, IGlovesEquip
    {
        public override int SubItemType => (int)ArmorItemType.Gloves;
    }
}