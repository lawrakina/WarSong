using System;
using UnityEngine;


namespace Gui
{
    internal class CursorParticleSystemView : MonoBehaviour
    {
        /// <summary>
        /// https://github.com/mob-sakai/ParticleEffectForUGUI
        /// </summary>
        [SerializeField]
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void PlayParticle(Vector3 vector3)
        {
            transform.position = vector3;
            _particleSystem.Play();
        }
    }
}