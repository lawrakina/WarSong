using System;
using Code.Extension;
using Data;
using Enums;
using Unit;
using Unit.Player;


namespace CharacterCustomizing
{
    public class PlayerClassesInitialization
    {
        #region Fields

        private readonly PlayerClassesData _data;
        private CharacterSettings _characterSettings;
        private IPlayerView _playerView;

        #endregion


        #region ClassLiveCycles

        public PlayerClassesInitialization(PlayerClassesData data)
        {
            _data = data;
        }

        #endregion


        #region Init

        public void Initialization(IPlayerView playerView, CharacterSettings characterSettings)
        {
            _playerView = playerView;
            _characterSettings = characterSettings;
            
            playerView.UnitVision = characterSettings.unitVisionParameters;
            playerView.Attributes = new UnitAttributes();
            
            playerView.UnitReputation = new UnitReputation();
            playerView.UnitReputation.FriendLayer = LayerManager.PlayerLayer;
            playerView.UnitReputation.EnemyLayer = LayerManager.EnemyLayer;
            playerView.UnitReputation.FriendAttackLayer = LayerManager.PlayerAttackLayer;
            playerView.UnitReputation.EnemyAttackLayer = LayerManager.EnemyAttackLayer;
            
            _playerView.UnitVision = _characterSettings.unitVisionParameters;
            var playerLevel = _playerView.UnitLevel.CurrentLevel;
            switch (_characterSettings.CharacterClass)
            {
                case CharacterClass.Warrior:
                    _playerView.CharacterClass = new CharacterClassWarrior();
                    GenerateStats(playerLevel, _data.Warrior);
                    break;

                case CharacterClass.Rogue:
                    _playerView.CharacterClass = new CharacterClassRogue();
                    GenerateStats(playerLevel, _data.Rogue);
                    break;

                case CharacterClass.Hunter:
                    _playerView.CharacterClass = new CharacterClassHunter();
                    GenerateStats(playerLevel, _data.Hunter);
                    break;

                case CharacterClass.Mage:
                    _playerView.CharacterClass = new CharacterClassMage();
                    GenerateStats(playerLevel, _data.Mage);
                    break;

                default:
                    Dbg.Error($"PlayerClassesInitialization.Initialization._characterSettings.CharacterClass:{_characterSettings.CharacterClass}");
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion


        #region Methods

        private void GenerateStats(int level, BasicCharacteristics value)
        {
            _playerView.UnitCharacteristics = new UnitCharacteristics
            {
                Start = value.Start,
                ForOneLevel = value.ForOneLevel,
                Values = new BasicCharacteristics.Characteristics(
                    value.Start.Strength + level * value.ForOneLevel.Strength,
                    value.Start.Agility +    level * value.ForOneLevel.Agility,
                    value.Start.Stamina +  level * value.ForOneLevel.Stamina,
                    value.Start.Intellect +level * value.ForOneLevel.Intellect,
                    value.Start.Spirit +      level * value.ForOneLevel.Spirit
                )
            };
            
            _playerView.UnitCharacteristics.Speed = _data.PlayerMoveSpeed;
            _playerView.UnitCharacteristics.RotateSpeedPlayer = _data.RotateSpeedPlayer;
            
            _playerView.UnitHealth = new UnitHealth();
            
            switch (_playerView.CharacterClass.ResourceType)
            {
                case ResourceEnum.Rage:
                    _playerView.CharacterClass.ResourceBaseValue = _data.maxRageValue;
                    break;

                case ResourceEnum.Energy:
                    _playerView.CharacterClass.ResourceBaseValue = _data.maxEnergyValue;
                    break;

                case ResourceEnum.Concentration:
                    _playerView.CharacterClass.ResourceBaseValue = _data.maxConcentrationValue;
                    break;

                case ResourceEnum.Mana:
                    _playerView.CharacterClass.ResourceBaseValue =
                        _playerView.UnitCharacteristics.ForOneLevel.Intellect * _data.manaPointsPerIntellect;
                    break;

                //todo сделать пересчет при получении уровня
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}