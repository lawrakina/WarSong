using Code.Profile;
using Code.Profile.Models;


namespace Code.Fight
{
    public sealed class EnemyFightController: BaseController
    {
        private readonly DungeonGeneratorModel _dungeonGeneratorModel;
        private readonly EnemiesLevelModel _enemiesLevelModel;

        public EnemyFightController(DungeonGeneratorModel dungeonGeneratorModel, EnemiesLevelModel enemiesLevelModel)
        {
            _dungeonGeneratorModel = dungeonGeneratorModel;
            _enemiesLevelModel = enemiesLevelModel;
        }

        public void SpawnEnemies()
        {
            //ToDo создавай врагов тут, бери настройки уровня в _dungeonGeneratorModel и после создания записывай их в _enemiesLevelModel.
            // тебе понадобятся фабрики по созданию врагов.
        }
    }
}