using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = "Config_BattleInputData", menuName = "Data/Config Battle Input Data")]
    public sealed class BattleInputData : ScriptableObject
    {
        public float _maxPressTimeForClickButton = 0.5f;
        public Vector3 _maxOffsetForClick = new Vector3(0.2f, 0.2f, 0.2f);
        public Vector3 _maxOffsetForMovement = new Vector3(0.2f, 0.2f, 0.2f);
        public UltimateJoystick _joystick;
    }
}