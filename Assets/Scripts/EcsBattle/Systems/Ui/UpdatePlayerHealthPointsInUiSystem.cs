using System;
using EcsBattle.Components;
using Leopotam.Ecs;
using Models;


namespace EcsBattle.Systems.Ui
{
    public class UpdatePlayerHealthPointsInUiSystem : IEcsRunSystem
    {
        #region Fields

        private EcsFilter<PlayerComponent, UnitHpComponent> _filter;
        private BattlePlayerModel _playerModel;

        #endregion


        public void Run()
        {
            foreach (var index in _filter)
            {
                _playerModel.CurrentHp = (float) Math.Ceiling(_filter.Get2(index).CurrentValue);
                _playerModel.MaxHp = (float) Math.Ceiling(_filter.Get2(index).MaxValue);
            }
        }
    }
}