using System;
using System.Collections.Generic;
using Code.Data.Abilities;
using Code.Fight.EcsFight.Settings;
using Code.UI.Fight;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Abilities{
    public struct HubOfAbilitiesTag{
        public EcsEntity OwnerEntity;
        public UnitC Owner;
        public Queue<Ability> Source;
    }

    public struct PermissionForAbilitiesTag{
    }

    public struct StartAbilityCommand{
    }

    public struct NeedMoveToTargetC{
    }

    public struct AbilityC{
        public Ability Value;
    }

    public struct NeedStartAbilityFromMeToTargetCommand{
    }
    
    public struct NeedCanceledAbilityTag{
    }

    public struct NeedCheckResource{
    }
}