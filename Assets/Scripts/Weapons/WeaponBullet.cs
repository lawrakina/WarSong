using System;
using Unit;
using UnityEngine;


namespace Weapons
{
    public class WeaponBullet : MonoBehaviour
    {
        public IBaseUnitView Target { get; set; }
        public float Speed = 50.0f;
        public TrailRenderer Trail {get; set; }

        private void Awake()
        {
            Trail = GetComponentInChildren<TrailRenderer>();
        }
    }
}