using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    [Serializable] public sealed class PresetCharacters
    {
        [SerializeField]
        public List<CharacterSettings> listPresetsSettings;
    }
}