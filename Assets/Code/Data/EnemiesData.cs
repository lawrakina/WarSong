using Code.Data.Unit.Enemy;
using UnityEngine;


namespace Code.Data
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Configs/Enemies Data")]
    public sealed class EnemiesData : ScriptableObject
    {
        [SerializeField]
        public EnemySettings[] Enemies;
    }
}