using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    [Serializable] public sealed class Characters
    {
        [SerializeField]
        public List<CharacterSettings> ListCharacters;
    }
}