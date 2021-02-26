using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "BattleInputData", menuName = "Data/Battle Input Data")]
    public sealed class BattleInputData : ScriptableObject
    {
        public float _maxPressTimeForClickButton = 0.5f;
        public Vector3 _maxOffsetForClick = new Vector3(0.2f, 0.2f, 0.2f);
        public UltimateJoystick _joystick;
        // = UltimateJoystick.GetUltimateJoystick(StringManager.ULTIMATE_JOYSTICK_MOVENMENT);
    }
}