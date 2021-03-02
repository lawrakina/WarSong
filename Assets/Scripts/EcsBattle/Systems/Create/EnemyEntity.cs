using Battle;
using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using Unit.Enemies;


namespace EcsBattle.Systems.Create
{
    public class EnemyEntity
    {
        #region Fields

        private readonly IEnemyView _view;
        private readonly EcsEntity _entity;

        #endregion


        #region ClassLiveCycles

        public EnemyEntity(IEnemyView view, EcsEntity entity)
        {
            _view = view;
            _entity = entity;

            _view.OnApplyDamageChange += ViewOnApplyDamageChange;
            Dbg.Log($"Create EnemyEnity");
        }

        ~EnemyEntity()
        {
            _view.OnApplyDamageChange -= ViewOnApplyDamageChange;
            Dbg.Log($"Destroy EnemyEnity");
        }

        #endregion


        #region Methods

        private void ViewOnApplyDamageChange(InfoCollision collision)
        {
            _entity.Get<AttackCollisionComponent>().Value = collision;
        }

        #endregion
    }
}