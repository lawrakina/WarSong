using UnityEngine;


namespace Code.Data{
    [CreateAssetMenu(fileName = nameof(GlobalUiAnimationSettings), menuName = "Configs/" + nameof(GlobalUiAnimationSettings))]
    public class GlobalUiAnimationSettings : ScriptableObject{
        [Header("Ui animation settings")]
        [Space]
        [Header("Ability button in Fight")]
        public float AbilButShakeDuration = 0.1f;
        public float AbilButShakeStrength = 2f;
        public int AbilButShakeVibrato = 1;

        public static float ABILITY_BUTTON_SHAKE_DURATION;
        public static float ABILITY_BUTTON_SHAKE_STRENGTH;
        public static int ABILITY_BUTTON_SHAKE_VIBRATO;
    }
}