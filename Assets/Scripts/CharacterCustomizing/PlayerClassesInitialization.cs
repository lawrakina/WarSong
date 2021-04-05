using System;
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
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion


        #region Methods

        private void GenerateStats(int level, BasicCharacteristics currentCharacteristits)
        {
            _playerView.BasicCharacteristics = new BasicCharacteristics
            {
                // Strength = charLevel * data.Strength,
                // Agility = charLevel * data.Agility,
                StaminaForLevel = level * currentCharacteristits.StaminaForLevel,
                IntellectForLevel = level * currentCharacteristits.IntellectForLevel,
                // Spirit = charLevel * data.Spirit
            };
            _playerView.BaseHp =
                _playerView.BasicCharacteristics.StartHp +
                _playerView.BasicCharacteristics.StartStamina * _data.HealthPerStamina +
                _playerView.BasicCharacteristics.StaminaForLevel *
                _data.HealthPerStamina;
            _playerView.CurrentHp = _playerView.BaseHp;
            _playerView.MaxHp = _playerView.BaseHp;

            // Dbg.Log($"SET CurrentHP: {_playerView.CharacterClass.CurrentHp}, MaxHp:{_playerView.CharacterClass.MaxHp}");

            switch (_playerView.CharacterClass.ResourceType)
            {
                case ResourceEnum.Rage:
                    _playerView.CharacterClass.ResourceBaseValue = _data.MaxRageValue;
                    break;

                case ResourceEnum.Energy:
                    _playerView.CharacterClass.ResourceBaseValue = _data.MaxEnergyValue;
                    break;

                case ResourceEnum.Concentration:
                    _playerView.CharacterClass.ResourceBaseValue = _data.MaxConcentrationValue;
                    break;

                case ResourceEnum.Mana:
                    _playerView.CharacterClass.ResourceBaseValue =
                        _playerView.BasicCharacteristics.IntellectForLevel * _data.ManaPointsPerIntellect;
                    break;

                //todo сделать пересчет при получении уровня
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}