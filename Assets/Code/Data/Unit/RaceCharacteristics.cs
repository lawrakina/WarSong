using System;
using UnityEngine;

namespace Code.Data.Unit
{
    [Serializable]
    public class RaceCharacteristics
    {
        [SerializeField] 
        private CharacterRace _race;
        [SerializeField]
        private Vector3 _scaleModelByRace = Vector3.one;

        public CharacterRace Race => _race;
        public Vector3 ScaleModelByRace => _scaleModelByRace;
    }
}