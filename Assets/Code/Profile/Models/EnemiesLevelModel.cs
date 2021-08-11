using System.Collections.Generic;
using Code.Unit;
using UnityEngine;


namespace Code.Profile.Models
{
    [CreateAssetMenu(fileName = nameof(EnemiesLevelModel), menuName = "Models/" + nameof(EnemiesLevelModel))]
    public class EnemiesLevelModel : ScriptableObject
    {
        private List<IEnemyView> _enemies = new List<IEnemyView>(); 
        public List<IEnemyView> Enemies => _enemies;
    }
}