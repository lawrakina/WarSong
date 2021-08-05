using Code.Profile.Models;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsBattle.Input
{
    public class UpdatePlayerHealthPointsInUiSystem : IEcsRunSystem
    {
        #region Fields

        private EcsFilter<PlayerComponent, UnitComponent> _filter;
        private InOutControlFightModel _model;

        #endregion


        public void Run()
        {
            foreach (var index in _filter)
            {
                _model.PlayerStats.CurrentHp = Mathf.CeilToInt(_filter.Get2(index)._health.CurrentHp);
                _model.PlayerStats.MaxHp =  Mathf.CeilToInt(_filter.Get2(index)._health.MaxHp);
            }
        }
    }
}