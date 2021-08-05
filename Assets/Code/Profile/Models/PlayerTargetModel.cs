using System;
using Code.Unit;


namespace Code.Profile.Models
{
    public class PlayerTargetModel
    {
        public Action<IBaseUnitView> ChangeTarget;
        public Action<int> ChangeMaxHp;
        public Action<int> ChangeCurrentHp;
    }
}