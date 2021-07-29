using EcsBattle.Components;
using Leopotam.Ecs;
using Unit;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.CustomEntities
{
    public class GoalEntity
    {
        #region Fields

        private readonly GoalLevelView _view;
        private readonly EcsEntity _entity;

        #endregion


        #region ClassLiveCycles

        public GoalEntity(GoalLevelView view, EcsEntity entity)
        {
            _view = view;
            _entity = entity;

            _view.GoalTriggerEnter += ViewGoalEnter;
        }

        ~GoalEntity()
        {
            _view.GoalTriggerEnter -= ViewGoalEnter;
        }

        #endregion


        private void ViewGoalEnter(GameObject obj)
        {
            if (obj.TryGetComponent(typeof(IPlayerView), out _))
                _entity.Get<GoalLevelAchievedComponent>();
        }
    }
}