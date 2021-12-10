using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Code.UI.Fight{
    public sealed class FightView : UiWindow{
        [SerializeField]
        private Button _pauseButton;

        [Header("Player UI")]
        [SerializeField]
        private Image _person;
        [SerializeField]
        private Text _namePlayer;
        [SerializeField]
        private Text _healthCountByPlayer;
        [SerializeField]
        private Text _resourceCountByPlayer;
        [SerializeField]
        private Image _healthBar;
        [SerializeField]
        private Image _resourceBar;
        [Space]
        [Header("Controls")]
        [SerializeField]
        private List<FightAbilityButton> listOfButtonAbilities;
        [SerializeField]
        private UltimateJoystick _joystick;

        private float _playerMaxHp = 0f;
        private float _playerCurrentHp = 0f;
        private float _playerMaxResource = 0f;
        private float _playerCurrentResource = 0f;

        public List<FightAbilityButton> ListOfButtonAbilities => listOfButtonAbilities;
        public UltimateJoystick Joystick => _joystick;

        public float PlayerCurrentHp{
            get{ return _playerCurrentHp; }
            set{
                _playerCurrentHp = value;
                _healthCountByPlayer.text = $"{_playerCurrentHp}/{_playerMaxHp}";
                _healthBar.fillAmount = _playerCurrentHp / _playerMaxHp;
            }
        }
        public float PlayerMaxHp{
            get{ return _playerMaxHp; }
            set{
                _playerMaxHp = value;
                _healthCountByPlayer.text = $"{_playerCurrentHp}/{_playerMaxHp}";
                _healthBar.fillAmount = _playerCurrentHp / _playerMaxHp;
            }
        }
        public float PlayerCurrentResource{
            get{ return _playerCurrentResource; }
            set{
                _playerCurrentResource = value;
                _resourceCountByPlayer.text = $"{_playerCurrentResource}/{_playerMaxResource}";
                _resourceBar.fillAmount = _playerCurrentResource / _playerCurrentHp;
            }
        }
        public float PlayerMaxResource{
            get{ return _playerMaxResource; }
            set{
                _playerMaxResource = value;
                _resourceCountByPlayer.text = $"{_playerCurrentResource}/{_playerMaxResource}";
                _resourceBar.fillAmount = _playerCurrentResource / _playerMaxResource;
            }
        }
    }
}