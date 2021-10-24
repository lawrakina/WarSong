using Code.Data;
using Code.Equipment;


namespace Code.Fight.EcsFight.Battle{
    public struct WeaponC{
        public EquipCellType Type;
        public WeaponBullet Bullet;
        public bool CanUse;
        public float CurrentCooldownTime;
        public float MaxCooldownTime;
        public UnitC Owner;
    }
}