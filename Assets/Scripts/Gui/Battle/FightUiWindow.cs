using System;
using Windows;
using Controller;
using Extension;
using Models;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Battle
{
    public sealed class FightUiWindow : UiWindow
    {
        [Header("Player")]
        [SerializeField]
        private Image _playerHpFiel;

        // public Image PlayerResourceFiel;
        [SerializeField]
        private Text _playerHpLabel;


        [Header("Target")]
        [SerializeField]
        private Image _targetBackground;

        [SerializeField]
        private Text _targetName;
        
        [SerializeField]
        private Image _targetHpFiel;

        // public Image _targetResourceFiel;
        [SerializeField]
        private Text _targetHpLabel;


        [Header("Rewards panel")]
        [SerializeField]
        private Image _timerIco;

        [SerializeField]
        private Text _timerText;

        [SerializeField]
        private Image _enemyIco;

        [SerializeField]
        private Text _enemyText;

        [SerializeField]
        private Image _bagIco;

        [SerializeField]
        private Text _bagText;

        [SerializeField]
        private Image _banditIco;

        [SerializeField]
        private Text _banditText;

        private BattlePlayerModel _playerModel;
        private BattleProgressModel _battleModel;
        private BattleTargetModel _targetModel;
        private int _maxBandit;
        private int _playerMaxHp;
        private int _maxEnemy;
        private int _currentEnemy;
        private float _maxBag;
        private int _maxTimer;
        private int _targetMaxHp;


        #region Player

        private int PlayerMaxHpValue
        {
            get => _playerMaxHp;
            set => _playerMaxHp = value;
        }

        private int PlayerCurrentHpValue
        {
            set
            {
                _playerHpLabel.text = $"{value}/{PlayerMaxHpValue}";
                _playerHpFiel.fillAmount = (float) value / (float) PlayerMaxHpValue;
            }
        }

        #endregion


        #region Target

        private int TargetMaxHpValue
        {
            get => _targetMaxHp;
            set => _targetMaxHp = value;
        }

        private int TargetCurrentHpValue
        {
            set
            {
                if (value > 0)
                {
                    _targetBackground.color = Color.red;
                    _targetHpLabel.text = $"{value}/{TargetMaxHpValue}";
                    _targetHpFiel.fillAmount = (float) value / (float) TargetMaxHpValue;
                }
                else
                {
                    _targetBackground.color = Color.grey;
                    _targetHpFiel.fillAmount = 0;
                    _targetHpLabel.text = StringManager.UNIT_STATUS_DEAD;
                }
            }
        }

        #endregion


        #region Reward panel

        public int MaxTimer
        {
            get => _maxTimer;
            set
            {
                if (value < 1)
                {
                    _timerIco.enabled = false;
                    _timerText.enabled = false;
                    return;
                }

                _maxTimer = value;
            }
        }

        public float CurrentTimer
        {
            set
            {
                var currentTimer = new DateTime();
                if (value < MaxTimer)
                {
                    _timerIco.color = Color.white;
                    _timerText.text = $"{currentTimer.AddSeconds(MaxTimer - Mathf.RoundToInt(value)):mm:ss}";
                }
                else
                {
                    _timerIco.color = Color.red;
                    _timerText.text = $"-{currentTimer.AddSeconds(Mathf.RoundToInt(value) - MaxTimer):mm:ss}";
                }
            }
        }


        public float MaxBag
        {
            get => _maxBag;
            set
            {
                if (value < 1)
                {
                    _bagIco.enabled = false;
                    _bagText.enabled = false;
                    return;
                }

                _maxBag = value;
            }
        }

        public float CurrentBag
        {
            set
            {
                if (value < MaxBag)
                {
                    _bagIco.color = Color.white;
                }
                else
                {
                    _bagIco.color = Color.green;
                }

                _bagText.text = $"{value}/{MaxBag}";
            }
        }

        public int MaxEnemy
        {
            get => _maxEnemy;
            set
            {
                if (value < 1)
                {
                    _enemyIco.enabled = false;
                    _enemyText.enabled = false;
                    return;
                }

                _maxEnemy = value;
                _enemyText.text = $"{CurrentEnemy}/{MaxEnemy}";
            }
        }

        public int CurrentEnemy
        {
            get => _currentEnemy;
            set
            {
                _currentEnemy = value;
                if (value < MaxEnemy)
                {
                    _enemyIco.color = Color.white;
                }
                else
                {
                    _enemyIco.color = Color.green;
                }

                _enemyText.text = $"{value}/{MaxEnemy}";
            }
        }


        public int MaxBandit
        {
            private get { return _maxBandit; }
            set
            {
                if (value < 1)
                {
                    _banditIco.enabled = false;
                    _banditText.enabled = false;
                }
                else
                {
                    _banditIco.enabled = true;
                    _banditText.enabled = true;
                }

                _maxBandit = value;
            }
        }

        public int CurrentBandit
        {
            set
            {
                if (value < MaxBandit)
                {
                    _banditIco.color = Color.white;
                }
                else
                {
                    _banditIco.color = Color.green;
                }

                _banditText.text = $"{value}/{MaxBandit}";
            }
        }

        #endregion


        public void SetModels(BattleProgressModel battleModel, BattlePlayerModel playerModel,
            BattleTargetModel targetModel)
        {
            _playerModel = playerModel;
            _battleModel = battleModel;
            _targetModel = targetModel;

            _playerModel.ChangeMaxHp += f => { PlayerMaxHpValue = f; };
            _playerModel.ChangeCurrentHp += f => { PlayerCurrentHpValue = f; };

            _battleModel.ChangeMaxTimer += f => { MaxTimer = f; };
            _battleModel.ChangeCurrentTimer += f => { CurrentTimer = f; };

            _battleModel.ChangeMaxEnemy += f => { MaxEnemy = f; };
            _battleModel.ChangeCurrentEnemy += f => { CurrentEnemy = f; };

            _battleModel.ChangeMaxBag += f => { MaxBag = f; };
            _battleModel.ChangeCurrentBag += f => { CurrentBag = f; };

            _battleModel.ChangeMaxBandit += f => { MaxBandit = f; };
            _battleModel.ChangeCurrentBandit += f => { CurrentBandit = f; };

            _targetModel.ChangeTarget += target =>
            {
                _targetBackground.gameObject.SetActive(target != null);
                _targetName.text = target?.Transform.gameObject.name;
            };
            _targetModel.ChangeMaxHp += f => { TargetMaxHpValue = f; };
            _targetModel.ChangeCurrentHp += f => { TargetCurrentHpValue = f; };
        }
    }
}