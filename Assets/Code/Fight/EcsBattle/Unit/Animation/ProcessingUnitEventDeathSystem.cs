using Code.Fight.EcsBattle.Statistics;
using Leopotam.Ecs;


namespace Code.Fight.EcsBattle.Unit.Animation
{
    public sealed class ProcessingUnitEventDeathSystem : IEcsRunSystem
    {    //ToDo optimisation Ragdoll for mobile =>
        //https://habr.com/ru/post/334922/
        private EcsFilter<UnitComponent, DeathEventComponent, ListRigidBAndCollidersComponent> _filter;
        
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var unit = ref _filter.Get1(i);
                ref var deathEvent = ref _filter.Get2(i);
                ref var listBbodies = ref _filter.Get3(i);
                // ref var listOfAwards = ref _filter.Get2(i);
            
                //Enable Ragdoll
                for (int j = 0; j < listBbodies._rigidBodies.Count; j++)
                {
                    listBbodies._rigidBodies[j].isKinematic = false;
                    listBbodies._colliders[j].enabled = true;
                }
                
                unit._animator.Off();
                unit._collider.enabled = false;
                unit._rigidBody.isKinematic = true;

                entity.Get<NeedToAddToStatisticsComponent>().killer = deathEvent._killer;
                // foreach (var rigidbody in listBbodies.rigidbodies)
                // {
                //     Dbg.Log($"Bone:{rigidbody.name}");
                //     rigidbody.isKinematic = false;
                // }
            }
        }
    }
}