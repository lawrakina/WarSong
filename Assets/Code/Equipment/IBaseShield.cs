using Code.Data;
using UnityEngine;


namespace Code.Equipment
{
    public interface IBaseShield
    {
        GameObject GameObject { get; }
        ArmorItemType ArmorItemType { get; }
    }
}