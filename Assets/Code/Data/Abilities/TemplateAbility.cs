using UnityEngine;


namespace Code.Data.Abilities{
    [CreateAssetMenu(fileName = nameof(TemplateAbility), menuName = "Abilities/" + nameof(TemplateAbility))]
    public class TemplateAbility : ScriptableObject{
        public UiInfo uiInfo;

        [Tooltip("Visual representation of the bullet")]
        public GameObject view;

        [Tooltip("Who is the target of the ability?")]
        public AbilityTargetType abilityTargetType;

        [Tooltip("What characteristic does it affect?")]
        public ChangeableCharacteristicType changeableCharacteristicType;

        [Tooltip("Required distance to action og spell\n2 = melee Attack")]
        [Range(2.0f, 100.0f)]
        public float distance = 2.0f;

        [Tooltip("Lag before apply ability")]
        [Range(0.0f, 1.0f)]
        public float timeLagBeforeAction = 0.5f;

        [Tooltip("Duration of the casting. -1 momentum")]
        [Range(-1.0f, 10.0f)]
        public float castTime = 0.0f;

        [Tooltip("Duration of the action")]
        [Range(0.0f, 24 * 60 * 60.0f)]
        public float timeOfAction = 0.0f;

        [Tooltip("Speed of approach to the target. -1 momentum")]
        [Range(-1.0f, 24 * 60 * 60.0f)]
        public float bulletSpeed = 0.0f;

        [Tooltip("Numerical effect on the parameter")]
        [Range(0.0f, 100000.0f)]
        public float value = 0.0f;

        [Tooltip("Ability full cooldown time")]
        [Range(0.0f, 60.0f)]
        public float cooldown = 0.0f;

        [Tooltip("Add the attack value from the weapon multiplied by the coefficient\n300% = 3.0, 0 = Disabled")]
        [Range(0.0f, 10.0f)]
        public float weaponValueModifier = 0.0f;

        [Tooltip("Required resource")]
        public ResourceEnum requiredResource;

        [Tooltip("Requires resource units")]
        [Range(0.0f, 10000.0f)]
        public float costResource = 10.0f;

        [Tooltip("Positive(x*1) or Negative(x*-1) effect")]
        public EffectValueType effectValueType = EffectValueType.Negative;

        [Tooltip("Target class")] //\nNone = permission for all")]
        public CharacterClass requiredClass;

        [Tooltip("Minimum required unit level")]
        public int requiredLevel = 0;
    }

    public enum AbilityTargetType{
        None,
        OnlyMe,
        OnlyTargetEnemy,
        OnlyTargetFriend,
        OnlyFriendlyWithoutMe,
        OnlyFriendlyWithMe,
        OnlyEnemies
    }

    public enum ChangeableCharacteristicType{
        HealthPoints,
        RagePoints,
        EnergyPoints,
        ConcentrationPoints,
        ManaPoints,
        LocalDeltaTime,
        StrengthPoints,
        AgilityPoints,
        StaminaPoints,
        IntellectPoints,
        SpiritPoints
    }

    public enum EffectValueType{
        Negative = -1,
        Positive = 1
    }

    public enum AbilityState{
        Ready, //ready to use
        Started, //task is created
        InProgress, 
        Completed,
        Cooldown,
        Canceled
    }
}