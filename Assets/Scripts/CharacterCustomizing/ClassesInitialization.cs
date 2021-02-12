using System;
using Data;
using Enums;
using Extension;
using Unit;
using Unit.Player;


namespace CharacterCustomizing
{
    public class ClassesInitialization
    {
        #region Fields

        private readonly ClassesData _data;
        private CharacterSettings _characterSettings;
        private IPlayerView _playerView;

        #endregion


        #region ctor

        public ClassesInitialization(ClassesData data)
        {
            _data = data;
        }

        #endregion


        #region Init

        public void Initialization(CharacterSettings characterSettings, IPlayerView playerView)
        {
            _playerView = playerView;
            _characterSettings = characterSettings;

            var charLevel = _characterSettings.Level;
            switch (_characterSettings.CharacterClass)
            {
                case CharacterClass.Warrior:
                    _playerView.CharacterClass = new CharacterClassWarrior();
                    GenerateStats(charLevel, _data.Warrior);
                    break;

                case CharacterClass.Rogue:
                    _playerView.CharacterClass = new CharacterClassRogue();
                    GenerateStats(charLevel, _data.Rogue);
                    break;

                case CharacterClass.Hunter:
                    _playerView.CharacterClass = new CharacterClassHunter();
                    GenerateStats(charLevel, _data.Hunter);
                    break;

                case CharacterClass.Mage:
                    _playerView.CharacterClass = new CharacterClassMage();
                    GenerateStats(charLevel, _data.Mage);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion


        #region Methods

        private void GenerateStats(int charLevel, BasicCharacteristics data)
        {
            _playerView.BasicCharacteristics = new BasicCharacteristics
            {
                // Strength = charLevel * data.Strength,
                // Agility = charLevel * data.Agility,
                Stamina = charLevel * data.Stamina,
                Intellect = charLevel * data.Intellect,
                // Spirit = charLevel * data.Spirit
            };
            _playerView.CharacterClass.BaseHp = _playerView.BasicCharacteristics.Stamina * 10;
            _playerView.CharacterClass.CurrentHp = _playerView.CharacterClass.BaseHp;

            Dbg.Log($"SET CurrentHP: {_playerView.CharacterClass.CurrentHp}, MaxHp:{_playerView.CharacterClass.MaxHp}");

            switch (_playerView.CharacterClass.ResourceType)
            {
                case ResourceEnum.Rage:
                    _playerView.CharacterClass.ResourceBaseValue = 100.0f;
                    break;

                case ResourceEnum.Energy:
                    _playerView.CharacterClass.ResourceBaseValue = 100.0f;
                    break;

                case ResourceEnum.Concentration:
                    _playerView.CharacterClass.ResourceBaseValue = 100.0f;
                    break;

                case ResourceEnum.Mana:
                    _playerView.CharacterClass.ResourceBaseValue = _playerView.BasicCharacteristics.Intellect * 15;
                    break;

                //todo сделать пересчет при получении уровня
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}