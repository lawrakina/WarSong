using System.Collections.Generic;
using System.Linq;
using Code.Data.Marker;
using Code.Data.Unit.Enemy;
using Code.Extension;
using UnityEngine;


namespace Code.Unit.Factories
{
    public class EnemyFactory : BaseController
    {
        private readonly EnemiesData _settings;
        private List<EnemyView> listOfEnemies = new List<EnemyView>();
        public EnemyFactory(EnemiesData settings)
        {
            _settings = settings;
          
        }

        public EnemyView CreateEnemy(SpawnMarkerEnemyInDungeon marker)
        {
            var item = _settings.Enemies.FirstOrDefault(x => x.EnemyType == marker._type);
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
             
            enemyView.Transform.SetParent(marker.Transform);
            enemyView.Transform.localPosition = Vector3.zero;
            enemyView.Transform.rotation = Quaternion.identity;
             
            return enemyView;
        }
    }
}