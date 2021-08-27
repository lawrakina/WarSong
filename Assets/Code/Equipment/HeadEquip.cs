using System;
using System.Collections.Generic;
using Code.Data;
using UnityEngine;


namespace Code.Equipment
{
    [Serializable]
    public class HeadEquip : BaseArmorItem, IHeadEquip
    {
        public override int SubItemType => (int)ArmorItemType.Head;
    }
}