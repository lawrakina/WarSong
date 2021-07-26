using Code.Extension;
using Controller;
using Data;
using Unit.Player;
using UnityEngine;


namespace Unit.Enemies
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyClassesData _data;
        private readonly EnemyClassesInitialization _enemyClassesInitialization;

        public EnemyFactory(EnemyClassesData data, EnemyClassesInitialization enemyClassesInitialization)
        {
            _data = data;
            _enemyClassesInitialization = enemyClassesInitialization;
        }

        public IEnemyView CreateEnemy(EnemySettings item)
        {
            var enemy = Object.Instantiate(item.EnemyView);
            enemy.name = $"Enemy.{item.EnemyType.ToString()}.{item.EnemyView.name}";
            var enemyView = enemy.AddCode<EnemyView>();
            enemyView.Animator = enemy.GetComponent<Animator>();
            enemyView.Transform = enemy.transform;
            enemyView.Rigidbody = enemy.AddRigidBody(80, CollisionDetectionMode.ContinuousSpeculative,
                false, true,
                RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ);
            enemyView.Collider = enemy.AddCapsuleCollider(0.5f, false,
                new Vector3(0.0f, 0.9f, 0.0f),
                1.8f);
            enemyView.MeshRenderer = enemy.GetComponent<MeshRenderer>();
            enemyView.AnimatorParameters = new AnimatorParameters(enemyView.Animator);

            // _customizerCharacter.Customize(enemyView, item);
            // _unitLevelInitialization.Initialization(enemyView, item);
            _enemyClassesInitialization.Initialization(enemyView, _data, item);

            // Dbg.Log($"view.UnitReputation.EnemyLayer:{enemyView.UnitReputation.EnemyLayer})");
            return enemyView;
        }
    }
}