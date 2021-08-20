using System.Collections.Generic;
using Code.Data;
using UnityEngine;


namespace Code.Equipment
{
    public interface IBaseArmor
    {
        GameObject GameObject { get; }
        int ArmorValue { get; }
        ArmorItemType ArmorItemType { get; }
        string NameInHierarchy { get; }
        List<GameObject> ListViews { get; }
    }
}