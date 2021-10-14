using System;
using UnityEngine;


namespace Code.Data.Unit
{
    [Serializable]
    public class PresetCharacterSettings : CharacterSettings, ICloneable
    {
        [SerializeField]
        public GameObject UiBadge;

        public ResourceEnum ResourceType;
        public float ResourceBaseValue;

        public object Clone()
        {
            return new PresetCharacterSettings
            {
                UiBadge = this.UiBadge,
                CharacterClass = this.CharacterClass,
                CharacterGender = this.CharacterGender,
                CharacterRace = this.CharacterRace,
                ExperiencePoints = this.ExperiencePoints,
                Equipment = this.Equipment,
                Abilities = this.Abilities,
                //глубокое копирование не требуется
            };
        }

        public CharacterSettings GetBase()
        {
            return (CharacterSettings) base.MemberwiseClone();
        }
    }
}