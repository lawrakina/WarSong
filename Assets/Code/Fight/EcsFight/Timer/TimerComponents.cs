namespace Code.Fight.EcsFight.Timer{
    public struct ClearTimer{
    }

    public struct Timer<TTimerFlag> where TTimerFlag : struct{
        public float TimeLeftSec;
    }

    public struct LagBeforeAttack1W{
    }

    public struct Reload1WeaponTag{
    }

    public struct BattleTag{
    }
}