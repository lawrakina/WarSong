using System;
using System.Collections.Generic;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable] public class PresetCharacters
    {
        [SerializeField]
        public List<PresetCharacterSettings> listPresetsSettings;
    }
}