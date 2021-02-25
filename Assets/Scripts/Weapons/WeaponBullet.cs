using UnityEngine;


namespace Weapons
{
    public class WeaponBullet : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; set; }
        private Collider Collider { get; set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Collider = GetComponent<Collider>();
        }
    }
}