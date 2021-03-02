using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Data/Enemies Data")]
    public sealed class EnemiesData : ScriptableObject
    {
        [SerializeField]
        public Enemies Enemies;

        //ToDo relocate UiEnemies to List<EnemySetting>
        [SerializeField]
        public UiEnemies UiEnemies;
    }
}