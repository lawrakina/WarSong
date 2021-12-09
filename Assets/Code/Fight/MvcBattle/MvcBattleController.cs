using Code.Data.Dungeon;
using Code.GameCamera;
using Code.Profile;
using Code.Profile.Models;
using Code.Unit;


namespace Code.Fight.MvcBattle{
    public class MvcBattleController : BaseController, IBattleController{
        private readonly Controllers _controllers;
        private readonly ProfilePlayer _profilePlayer;

        private BattleCamera _camera;
        private IPlayerView _player;
        private DungeonParams _dungeonParams;
        private EnemiesLevelModel _listEnemiesLevelModel;
        private InOutControlFightModel _inOutControlFightModel;
        
        private BattlePlayerController _playerController;

        public MvcBattleController(Controllers controllers, ProfilePlayer profilePlayer){
            _controllers = controllers;
            _profilePlayer = profilePlayer;
        }

        public void StartFight(){
            _playerController = ConfigurePlayerController(_player);
            // _cameraController = ConfigureCameraController(_camera);
            // _inputController = ConfigureInputController(_inOutControlFightModel);
        }

        private BattlePlayerController ConfigurePlayerController(IPlayerView player){
            var result = new BattlePlayerController(player);
            AddController(result);
            _controllers.Add(result);
            return result;
        }

        public void UnSaveStopBattle(){
            
        }


        public void Inject(object obj){
            if (obj is BattleCamera camera)
                _camera = camera;
            if (obj is IPlayerView view)
                _player = view;
            if (obj is DungeonParams dunParams)
                _dungeonParams = dunParams;
            if (obj is InOutControlFightModel inputModel)
                _inOutControlFightModel = inputModel;
            if (obj is EnemiesLevelModel listEnemies)
                _listEnemiesLevelModel = listEnemies;
        }
    }
}