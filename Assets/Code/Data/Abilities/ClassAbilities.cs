using System;
using System.Collections.Generic;


namespace Code.Data.Abilities{
    [Serializable]
    public class ClassAbilities{
        public CharacterClass Owner;
        public ActiveAbilitiesFromCharacter activeAbilitiesFromCharacter;
        public List<TemplateAbility> ListOfAbilities;
    }
}