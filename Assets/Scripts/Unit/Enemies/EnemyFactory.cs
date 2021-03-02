using Controller;
using Data;
using Extension;
using Unit.Player;
using UnityEngine;


namespace Unit.Enemies
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyClassesInitialization _enemyClassesInitialization;

        public EnemyFactory(EnemyClassesInitialization enemyClassesInitialization)
        {
            _enemyClassesInitialization = enemyClassesInitialization;
        }

        public IEnemyView CreateEnemy(EnemySettings item)
        {
            var enemy = Object.Instantiate(item.EnemyView);
            enemy.name = $"Enemy.{item.EnemyType.ToString()}.{item.EnemyView.name}";
            enemy.AddCapsuleCollider(0.5f, false,
                     new Vector3(0.0f, 0.9f, 0.0f),
                     1.8f)
                 .AddRigidBody(80, CollisionDetectionMode.ContinuousSpeculative,
                     false, true,
                     RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                     RigidbodyConstraints.FreezeRotationZ)
                 // .AddNavMeshAgent()
                 .AddCode<EnemyView>();

            var enemyView = enemy.GetComponent<IEnemyView>();

            // _customizerCharacter.Customize(enemyView, item);
            // _unitLevelInitialization.Initialization(enemyView, item);
            _enemyClassesInitialization.Initialization(enemyView, item);

            // Dbg.Log($"view.UnitReputation.EnemyLayer:{enemyView.UnitReputation.EnemyLayer})");
            return enemyView;
        }
    }
}