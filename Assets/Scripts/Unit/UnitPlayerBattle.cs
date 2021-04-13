using System;
using Enums;
using UnityEngine;
using Weapons;


namespace Unit
{
    [Serializable]
    public class UnitPlayerBattle
    {
        public BaseWeapon MainWeapon;
        public BaseWeapon SecondWeapon;
        public ActiveWeapons ActiveWeapons;
    }
}