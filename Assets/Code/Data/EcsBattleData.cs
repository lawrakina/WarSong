using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = "Config_EcsBattleData", menuName = "Data/Config EcsBattle Data")]
    public class EcsBattleData : ScriptableObject
    {
        [SerializeField]
        public EcsBattle.EcsBattle EcsBattle;
    }
}