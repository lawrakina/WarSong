﻿namespace Code.Data
{
    public enum CharacterClass
    {
        None,
        Warrior,
        Rogue,
        Hunter,
        Mage
    }

    public enum CharacterGender
    {
        Male,
        Female
    }

    public enum CharacterRace
    {
        Human,
        NightElf,
        BloodElf,
        Orc
    }

    public enum InventoryItemType
    {
        Weapon,
        Armor,
        QuestItem,
        Trash,
        Food,
    }

    public enum ArmorItemType
    {
        Shield,
        Head,
        Neck,
        Shoulder,
        Body,
        Tabard,
        Cloth,
        Bracelet,
        Gloves,
        Belt,
        Pants,
        Shoes,
        Ring,
        Earring
    }
    
    public enum WeaponItemType
    {
        None,
        OneHandWeapon,
        TwoHandSwordWeapon,
        TwoHandSpearWeapon,
        TwoHandStaffWeapon,
        RangeTwoHandBowWeapon,
        RangeTwoHandCrossbowWeapon,
        // RangeTwoHandGunWeapon,
        // ExtraWeapon,
    }
    public enum ActiveWeapons
    {
        RightHand,
        TwoHand,
        RightAndLeft,
        RightAndShield
    }
    public enum ResourceEnum
    {
        Rage,
        Energy,
        Concentration,
        Mana
    }
    public enum EnemyType
    {
        simple
    }

    public enum UiWindowAfterStart
    {
        Adventure,
        Characters,
        Inventory,
        Tavern,
        Shop,
        Tutorial,
        StartVideo,
        FuckOff
    }
}