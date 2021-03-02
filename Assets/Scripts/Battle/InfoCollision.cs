﻿namespace Battle
{
    public struct InfoCollision
    {
        #region Fields

        private readonly float _damage;

        // private readonly ContactPoint _contact;
        // private readonly Transform _objCollision;
        // private readonly Vector3 _direction;

        #endregion


        public InfoCollision(float damage)
        {
            _damage = damage;
        }
        // public InfoCollision(float damage, ContactPoint contact, Transform objCollision, Vector3 direction = default)
        // {
        //     _damage = damage;
        //     _direction = direction;
        //     _contact = contact;
        //     _objCollision = objCollision;
        // }


        #region Properties

        // public Vector3 Direction => _direction;

        public float Damage => _damage;

        // public ContactPoint Contact => _contact;
        //
        // public Transform ObjCollision => _objCollision;

        #endregion
    }
}