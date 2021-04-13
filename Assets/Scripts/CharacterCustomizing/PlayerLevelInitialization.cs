using Data;
using Extension;
using Unit;
using Unit.Player;


namespace CharacterCustomizing
{
    public class PlayerLevelInitialization
    {
        #region Fields

        private readonly PlayerLevelData _playerLevelData;
        private CharacterSettings _characterSettings;
        private IPlayerView _playerView;

        #endregion


        public PlayerLevelInitialization(PlayerLevelData playerLevelData)
        {
            _playerLevelData = playerLevelData;
        }

        public void Initialization(IPlayerView playerView, CharacterSettings characterSettings)
        {
            _playerView = playerView;
            _characterSettings = characterSettings;

            _playerView.UnitLevel = new UnitLevel();
            var balansExp = _characterSettings.ExperiencePoints;
            foreach (var level in _playerLevelData.Levels)
            {
                if (balansExp - level.MaxiPointsExperience >= 0)
                {
                    balansExp -= level.MaxiPointsExperience;
                    _playerView.UnitLevel.CurrentExperiencePoints = balansExp;
                    _playerView.UnitLevel.UpNextLevel();
                    // Dbg.Log($"Player.GetNextLevel. CurrentLevel:{_playerView.UnitLevel.CurrentLevel}");
                    // Dbg.Log($"Player.CurrentExpPoints:{_playerView.UnitLevel.CurrentExperiencePoints}");
                }
            }
        }
    }
}