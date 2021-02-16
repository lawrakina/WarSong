using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Data/Enemies Data")]
    public sealed class EnemiesData : ScriptableObject
    {
        [SerializeField]
        public Enemies Enemies;

        [SerializeField]
        public UiEnemies UiEnemies;
    }
}