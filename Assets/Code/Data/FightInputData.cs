using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = nameof(FightInputData), menuName = "Configs/" + nameof(FightInputData))]
    public sealed class FightInputData : ScriptableObject
    {
        [SerializeField]
        private float _maxPressTimeForClickButton = 0.5f;
        [SerializeField]
        private Vector3 _maxOffsetForClick = new Vector3(0.2f, 0.2f, 0.2f);
        [SerializeField]
        private Vector3 _maxOffsetForMovement = new Vector3(0.2f, 0.2f, 0.2f);

        public float MaxPressTimeForClickButton => _maxPressTimeForClickButton;
        public Vector3 MaxOffsetForClick => _maxOffsetForClick;
        public Vector3 MaxOffsetForMovement => _maxOffsetForMovement;
    }
}