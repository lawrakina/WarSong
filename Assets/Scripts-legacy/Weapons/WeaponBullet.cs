using System;
using Unit;
using UnityEngine;


namespace Weapons
{
    public class WeaponBullet : MonoBehaviour
    {
        #region fields

        public float Speed = 50.0f;
        private bool _isTrail;
        private bool _isParticleSystems;

        #endregion


        #region Properties

        private TrailRenderer Trail { get; set; }
        private ParticleSystem[] ParticleSystems { get; set; }
        public IBaseUnitView Target { get; set; }

        #endregion


        private void Awake()
        {
            Trail = GetComponentInChildren<TrailRenderer>();
            _isTrail = Trail != null;

            ParticleSystems = GetComponentsInChildren<ParticleSystem>();
            _isParticleSystems = ParticleSystems != null;
        }


        public void Clear()
        {
            if (_isTrail)
                Trail.Clear();
            if (_isParticleSystems)
                foreach (var particle in ParticleSystems)
                {
                    particle.Clear();
                }
        }
    }
}