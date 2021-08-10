using System.Collections.Generic;
using Code.Unit;
using UnityEngine;


namespace Code.Profile.Models
{
    [CreateAssetMenu(fileName = nameof(EnemiesLevelModel), menuName = "Models/" + nameof(EnemiesLevelModel))]
    public class EnemiesLevelModel : ScriptableObject
    {
        [SerializeField] private List<EnemyView> _enemies;
        public List<EnemyView> Enemies => _enemies;
    }
}