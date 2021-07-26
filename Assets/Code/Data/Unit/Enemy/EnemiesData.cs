using UnityEngine;


namespace Code.Data.Unit.Enemy
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Data/Enemies Data")]
    public sealed class EnemiesData : ScriptableObject
    {
        [SerializeField]
        public EnemySettings[] Enemies;
    }
}