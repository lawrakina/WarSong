using System;
using Code.Unit;


namespace Code.Profile.Models
{
    public class PlayerTargetModel
    {
        public Action<IBaseUnitView> ChangeTarget = view => { };
        public Action<int> ChangeMaxHp = i => { };
        public Action<int> ChangeCurrentHp = i => { };
    }
}