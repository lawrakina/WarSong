using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Timer{
    public struct ClearTimer{
    }

    public struct Timer<TTimerFlag> where TTimerFlag : struct{
        public float TimeLeftSec;
    }

    public struct LagBeforeAttackWeapon<T>{
    }

    public struct BattleTag{
    }
    
    public struct TimerForAdd{
        public float TimeLeftSec;
        public EcsEntity TargetEntity;
    }
}