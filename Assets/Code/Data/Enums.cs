namespace Code.Data
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
        Elf,
        Gnome,
        Orc
    }

    public enum InventoryItemType
    {
        None,
        EquipItem,
        QuestItem,
        Trash,
        Food,
    }

    public enum EquipCellType
    {
        MainHand,
        SecondHand,
        Head,
        Neck,
        Shoulder,
        Body,
        Cloak,
        Bracelet,
        Gloves,
        Belt,
        Pants,
        Shoes,
        Ring1,
        Ring2,
        Earring1,
        Earring2
    }

    public enum TargetEquipCell
    {
        OneHand,
        MainHand,
        SecondHand,
        TwoHand,
        Shield,
        Head,
        Neck,
        Shoulder,
        Body,
        Cloak,
        Bracelet,
        Gloves,
        Belt,
        Pants,
        Shoes,
        Ring,
        Earring
    }

    public enum EquipItemType
    {
        Armor,
        Weapon
    }

    public enum AbilityCellType{
        Special,
        Action1,
        Action2,
        Action3,
        IsStock
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
    public enum HeavyLightMedium
    {
        NoRequire,
        Heavy,
        Medium,
        Light
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

    public enum GoalType
    {
        Final,
        Secret,
        ToHome
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

    public enum CameraAngles{
        BeforePlayer,
        AfterPlayer,
        InTheFace
    }
}