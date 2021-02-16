using Data;
using Extension;
using Unit.Player;


namespace CharacterCustomizing
{
    public class UnitLevelInitialization
    {
        #region Fields

        private readonly UnitLevelData _unitLevelData;
        private CharacterSettings _characterSettings;
        private IPlayerView _playerView;

        #endregion


        public UnitLevelInitialization(UnitLevelData unitLevelData)
        {
            _unitLevelData = unitLevelData;
        }

        public void Initialization(IPlayerView playerView, CharacterSettings characterSettings)
        {
            _playerView = playerView;
            _characterSettings = characterSettings;

            var balansExp = characterSettings.ExperiencePoints;
            foreach (var level in _unitLevelData.Levels)
            {
                if (balansExp - level.MaxiPointsExperience >= 0)
                {
                    balansExp -= level.MaxiPointsExperience;
                    _playerView.Level.CurrentExperiencePoints = balansExp;
                    _playerView.Level.UpNextLevel();
                    Dbg.Log($"Player.GetNextLevel. CurrentLevel:{_playerView.Level.CurrentLevel}");
                    Dbg.Log($"Player.CurrentExpPoints:{_playerView.Level.CurrentExperiencePoints}");
                }
            }
        }
    }
}