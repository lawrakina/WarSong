using System;
using System.Collections.Generic;
using Code.Data.Abilities;
using Code.Fight.EcsFight.Settings;
using Code.UI.Fight;


namespace Code.Fight.EcsFight.Abilities{
    
    public struct HubOfAbilitiesTag{
        public UnitC Owner;
        public Queue<Ability> Source;
        // public Action<AbilityState> ChangeStateOfLastAbility;
    }
    
    public struct PermissionForAbilitiesTag{
    }
}