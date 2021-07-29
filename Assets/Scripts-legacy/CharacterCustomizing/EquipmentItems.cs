using Data;
using ItemsEquip;
using Weapons;


namespace CharacterCustomizing
{
    public class EquipmentItems
    {
        #region Properties

        public BaseWeapon MainWeapon { get; set; }
        public BaseWeapon SecondWeapon { get; set; }
        public BaseItemEquip Earring2Equip { get; set; }
        public BaseItemEquip Earring1Equip { get; set; }
        public BaseItemEquip Ring2Equip { get; set; }
        public BaseItemEquip Ring1Equip { get; set; }
        public BaseItemEquip BootsEquip { get; set; }
        public BaseItemEquip PantsEquip { get; set; }
        public BaseItemEquip EquipBelt { get; set; }
        public BaseItemEquip GlovesEquip { get; set; }
        public BaseItemEquip WristEquip { get; set; }
        public BaseItemEquip TabardEquip { get; set; }
        public BaseItemEquip ShirtEquip { get; set; }
        public BaseItemEquip ChestEquip { get; set; }
        public BaseItemEquip CloakEquip { get; set; }
        public BaseItemEquip ShoulderEquip { get; set; }
        public BaseItemEquip NeckEquip { get; set; }
        public BaseItemEquip HeadEquip { get; set; }

        public int SumItemsLevel
        {
            get
            {
                var result = 0;
                result += MainWeapon != null ? MainWeapon.ItemLevel : 0;
                result += SecondWeapon != null ? SecondWeapon.ItemLevel : 0;
                result += HeadEquip != null ? HeadEquip.ItemLevel : 0;
                result += NeckEquip != null ? NeckEquip.ItemLevel : 0;
                result += ShoulderEquip != null ? ShoulderEquip.ItemLevel : 0;
                result += CloakEquip != null ? CloakEquip.ItemLevel : 0;
                result += ChestEquip != null ? ChestEquip.ItemLevel : 0;
                result += ShirtEquip != null ? ShirtEquip.ItemLevel : 0;
                result += TabardEquip != null ? TabardEquip.ItemLevel : 0;
                result += WristEquip != null ? WristEquip.ItemLevel : 0;
                result += GlovesEquip != null ? GlovesEquip.ItemLevel : 0;
                result += EquipBelt != null ? EquipBelt.ItemLevel : 0;
                result += PantsEquip != null ? PantsEquip.ItemLevel : 0;
                result += BootsEquip != null ? BootsEquip.ItemLevel : 0;
                result += Ring1Equip != null ? Ring1Equip.ItemLevel : 0;
                result += Ring2Equip != null ? Ring2Equip.ItemLevel : 0;
                result += Earring1Equip != null ? Earring1Equip.ItemLevel : 0;
                result += Earring2Equip != null ? Earring2Equip.ItemLevel : 0;

                return result;
            }
        }

        #endregion
    }
}