using Code.Fight.EcsFight.Settings;
using Code.GameCamera;
using Code.Profile.Models;
using Leopotam.Ecs;


namespace Code.Fight.EcsFight.Create{
    public class CreateEnemiesS : IEcsInitSystem{
        private EcsWorld _world;
        private EnemiesLevelModel _enemiesModel;
        private BattleCamera _camera;
        private InOutControlFightModel _model;

        public void Init(){
            foreach (var view in _enemiesModel.Enemies){
                view.HealthBar.SetCamera(_camera.transform);

                var entity = _world.NewEntity();
                entity.Get<EnemyTag>();
                entity.Get<AnimatorTag>();
                ref var unit = ref entity.Get<UnitC>();
                entity.Get<UnitC>().UnitMovement = view.UnitMovement;
                entity.Get<UnitC>().Animator = view.AnimatorParameters;
                entity.Get<UnitC>().Characteristics = view.UnitCharacteristics;
                entity.Get<UnitC>().Health = view.UnitHealth;
                entity.Get<UnitC>().Resource = view.UnitResource;
                entity.Get<UnitC>().UnitVision = view.UnitVision;
                entity.Get<UnitC>().Reputation = view.UnitReputation;
                entity.Get<UnitC>().UnitLevel = view.UnitLevel;
                entity.Get<UiEnemyHealthBarC>().value = view.HealthBar;

                view.HealthBar.SetOnOff(false);
                //Ragdoll
                // SearchNodesOfRagdoll(entity, view);

                var enemyEntity = new EnemyEntity(view, entity);
            }
        }

        /*        private static void SearchNodesOfRagdoll(EcsEntity entity, IBaseUnitView view)
        {
            entity.Get<ListRigidBAndCollidersComponent>()._rigidBodies
                = new List<Rigidbody>(view.Transform.GetComponentsInChildren<Rigidbody>());
            entity.Get<ListRigidBAndCollidersComponent>()._colliders
                = new List<Collider>(view.Transform.GetComponentsInChildren<Collider>());
            foreach (var rigidbody in entity.Get<ListRigidBAndCollidersComponent>()._rigidBodies)
                rigidbody.isKinematic = true;
            foreach (var collider in entity.Get<ListRigidBAndCollidersComponent>()._colliders)
                collider.enabled = false;
            // view.Rigidbody.isKinematic = false;
            view.Collider.enabled = true;
        }*/
    }
}