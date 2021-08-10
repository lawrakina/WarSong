using UnityEngine;


namespace Code.Data.Unit.Enemy
{
    [CreateAssetMenu(fileName = nameof(EnemiesData), menuName = "Configs/" + nameof(EnemiesData))]
    public sealed class EnemiesData : ScriptableObject
    {
        [SerializeField]
        public EnemySettings[] Enemies;
    }
}