using Data;
using EcsBattle.Components;
using EcsBattle.CustomEntities;
using Leopotam.Ecs;
using Unit;


namespace EcsBattle
{
    public sealed class SpawnGoalLevelSystem : IEcsInitSystem
    {
        private BattleSettingsData _battleSettings;
        private GoalLevelView _view;
        private EcsWorld _world;

        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<GoalLevelComponent>();

            var goalEntity = new GoalEntity(_view, entity);
        }
    }
}