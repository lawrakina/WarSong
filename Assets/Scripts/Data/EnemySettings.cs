﻿using System;
using Enums;
using UnityEngine;


namespace Data
{
    [Serializable]
    public sealed class EnemySettings
    {
        [SerializeField]
        public GameObject EnemyView;
        [SerializeField]
        public EnemyType EnemyType;
    }
}