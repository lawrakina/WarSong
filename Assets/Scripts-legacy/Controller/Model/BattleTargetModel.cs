using System;
using Unit;


namespace Controller.Model
{
    public sealed class BattleTargetModel
    {
        public Action<IBaseUnitView> ChangeTarget;
        public Action<int> ChangeMaxHp;
        public Action<int> ChangeCurrentHp;
    }
}