using System;
using System.Collections.Generic;


namespace Code.Data.Abilities{
    [Serializable] public class ClassAbilities{
        public CharacterClass Owner;
        public List<KvpAbilityAndCellType> activeAbilitiesFromCharacter;
        public List<TemplateAbility> ListOfAbilities;
    }
}