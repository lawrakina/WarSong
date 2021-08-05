﻿using System;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable] public struct Characteristics
    {
        [SerializeField]
        public float Strength;

        [SerializeField]
        public float Agility;

        [SerializeField]
        public float Stamina;

        [SerializeField]
        public float Intellect;

        [SerializeField]
        public float Spirit;

        public Characteristics(
            float strength = 1.0f,
            float agility = 1.0f,
            float stamina = 1.0f,
            float intellect = 1.0f,
            float spirit = 1.0f)
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Spirit = spirit;
            Intellect = intellect;
        }
    }
}