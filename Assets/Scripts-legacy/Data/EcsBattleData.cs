using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "EcsBattleData", menuName = "Data/EcsBattle Data")]
    public class EcsBattleData : ScriptableObject
    {
        [SerializeField]
        public EcsBattle.EcsBattle EcsBattle;
    }
}