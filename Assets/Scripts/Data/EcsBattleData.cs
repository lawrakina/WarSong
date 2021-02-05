using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "EcsBattleData", menuName = "Data/EcsBattleData")]
    public class EcsBattleData : ScriptableObject
    {
        [SerializeField]
        public EcsBattle.EcsBattle EcsBattle;
    }
}