using System;
using Controller.Model;
using EcsBattle.Components;
using Leopotam.Ecs;
using Models;
using UnityEngine;


namespace EcsBattle.Systems.Ui
{
    public class UpdatePlayerHealthPointsInUiSystem : IEcsRunSystem
    {
        #region Fields

        private EcsFilter<PlayerComponent, UnitComponent> _filter;
        private BattlePlayerModel _playerModel;

        #endregion


        public void Run()
        {
            foreach (var index in _filter)
            {
                _playerModel.CurrentHp = Mathf.CeilToInt(_filter.Get2(index)._health.CurrentHp);
                _playerModel.MaxHp =  Mathf.CeilToInt(_filter.Get2(index)._health.MaxHp);
            }
        }
    }
}