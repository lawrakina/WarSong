using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    [Serializable]
    public sealed class Enemies
    {
        [SerializeField]
        public List<EnemySettings> ListEnemies;
    }
}