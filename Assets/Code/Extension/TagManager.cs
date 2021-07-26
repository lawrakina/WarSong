using UnityEngine;


namespace Code.Extension
{
    public class TagManager
    {
        public static string TAG_PLAYER = "Player";

        public static int ANIMATOR_PARAM_BATTLE = Animator.StringToHash("Battle");
        public static int ANIMATOR_PARAM_SPEED = Animator.StringToHash("Speed");
        public static int ANIMATOR_PARAM_HORIZONTAL_SPEED = Animator.StringToHash("HorizontalSpeed");
        public static int ANIMATOR_PARAM_WEAPON_TYPE = Animator.StringToHash("WeaponType");
        public static int ANIMATOR_PARAM_ATTACK_TYPE = Animator.StringToHash("AttackType");
        public static int ANIMATOR_PARAM_ATTACK_TRIGGER = Animator.StringToHash("Attack");
        public static int ANIMATOR_PARAM_FALLING = Animator.StringToHash("Falling");
        public static int ANIMATOR_PARAM_JUMP_TRIGGER = Animator.StringToHash("Jump");
        // public static int ANIMATOR_PARAM_WEAPON_UNSHEATH_TRIGGER = Animator.StringToHash("WeaponUnsheathTrigger");
        // public static int ANIMATOR_PARAM_WEAPON_SHEATH_TRIGGER = Animator.StringToHash("WeaponSheathTrigger");
        public static int ANIMATOR_PARAM_SWIM = Animator.StringToHash("State_Swim");
        public static int ANIMATOR_PARAM_FLY = Animator.StringToHash("State_Fly");
        public static int ANIMATOR_PARAM_STUNNED = Animator.StringToHash("State_Stunned");
        public static int ANIMATOR_PARAM_STATE_UNIT = Animator.StringToHash("StateUnit");
    }
}