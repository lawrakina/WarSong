using Code.Fight.EcsBattle;
using Code.Unit;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Create{
    public class PlayerEntity
    {
        #region Fields

        private readonly IPlayerView _view;
        private readonly EcsEntity _entity;

        #endregion


        #region ClassLiveCycles

        public PlayerEntity(IPlayerView view, EcsEntity entity)
        {
            _view = view;
            _entity = entity;

            _view.OnApplyDamageChange += ViewOnApplyDamageChange;
            // Dbg.Log($"Create EnemyEnity");
        }

        ~PlayerEntity()
        {
            _view.OnApplyDamageChange -= ViewOnApplyDamageChange;
            // Dbg.Log($"Destroy EnemyEnity");
        }

        #endregion


        #region Methods

        private void ViewOnApplyDamageChange(InfoCollision collision)
        {
            _entity.Get<AttackCollisionComponent>()._value = collision;
        }

        #endregion
    }
}