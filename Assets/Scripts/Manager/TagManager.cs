using UnityEngine;


namespace Manager
{
    public static class TagManager
    {
        public static string TAG_PLAYER = "Player";

        public static string TAG_PET_SPAWN_ZONE = "PetSpawnZone";
        // public static string AX_HORIZONTAL = "Horizontal";
        // public static string AX_VERTICAL = "Vertical";
        // public static string LAYER_MASK_ENVIRONMENT = "Environment";
        // public static string LAYER_MASK_UNITS = "Units";

        // public static string ANIMATOR_PARAM_BATTLE = "Battle";
        // public static string ANIMATOR_PARAM_MOVE = "Move";
        // public static string ANIMATOR_PARAM_SPEED = "Speed";
        // public static string ANIMATOR_PARAM_WEAPON_TYPE = "WeaponType";
        // public static string ANIMATOR_PARAM_ATTACK_TYPE = "AttackType";
        // public static string ANIMATOR_PARAM_ATTACK_TRIGGER = "Attack";
        // public static string ANIMATOR_PARAM_FALLING = "Falling";
        // public static string ANIMATOR_PARAM_JUMP_TRIGGER = "Jump";
        // public static string ANIMATOR_PARAM_WEAPON_UNSHEATH_TRIGGER = "WeaponUnsheathTrigger";
        // public static string ANIMATOR_PARAM_WEAPON_SHEATH_TRIGGER = "WeaponSheathTrigger";
        // public static string ANIMATOR_PARAM_SWIM = "State_Swim";
        // public static string ANIMATOR_PARAM_FLY = "State_Fly";
        // public static string ANIMATOR_PARAM_STUNNED = "State_Stunned";


        public static int ANIMATOR_PARAM_BATTLE = Animator.StringToHash("Battle");

        // public static int ANIMATOR_PARAM_MOVE  = Animator.StringToHash( "Move");
        public static int ANIMATOR_PARAM_SPEED = Animator.StringToHash("Speed");
        public static int ANIMATOR_PARAM_WEAPON_TYPE = Animator.StringToHash("WeaponType");
        public static int ANIMATOR_PARAM_ATTACK_TYPE = Animator.StringToHash("AttackType");
        public static int ANIMATOR_PARAM_ATTACK_TRIGGER = Animator.StringToHash("Attack");
        public static int ANIMATOR_PARAM_FALLING = Animator.StringToHash("Falling");
        public static int ANIMATOR_PARAM_JUMP_TRIGGER = Animator.StringToHash("Jump");
        public static int ANIMATOR_PARAM_WEAPON_UNSHEATH_TRIGGER = Animator.StringToHash("WeaponUnsheathTrigger");
        public static int ANIMATOR_PARAM_WEAPON_SHEATH_TRIGGER = Animator.StringToHash("WeaponSheathTrigger");
        public static int ANIMATOR_PARAM_SWIM = Animator.StringToHash("State_Swim");
        public static int ANIMATOR_PARAM_FLY = Animator.StringToHash("State_Fly");
        public static int ANIMATOR_PARAM_STUNNED = Animator.StringToHash("State_Stunned");
        public static int ANIMATOR_PARAM_STATE_UNIT = Animator.StringToHash("StateUnit");
    }
}