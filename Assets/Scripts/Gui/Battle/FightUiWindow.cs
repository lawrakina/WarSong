using System;
using Windows;
using Models;
using UnityEngine;
using UnityEngine.UI;


namespace Gui.Battle
{
    public sealed class FightUiWindow : UiWindow
    {
        public Image HpFiel;

        // public Image ResourceFiel;
        public Text HpLabel;

        public Image TimerIco;
        public Text TimerText;
        public Image EnemyIco;
        public Text EnemyText;
        public Image BagIco;
        public Text BagText;
        public Image BanditIco;
        public Text BanditText;

        private BattlePlayerModel _playerModel;
        private BattleProgressModel _battleModel;
        private int _maxBandit;
        private int _maxHp;
        private int _maxEnemy;
        private int _currentEnemy;


        private int MaxHpValue
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        private int CurrentHpValue
        {
            set
            {
                HpLabel.text = $"{value}/{MaxHpValue}";
                HpFiel.fillAmount = (float)value / (float)MaxHpValue;
            }
        }

        public int MaxTimer { get; set; }

        public float CurrentTimer
        {
            set
            {
                var currentTimer = new DateTime();
                if (value < MaxTimer)
                {
                    TimerIco.color = Color.white;
                    TimerText.text = $"{currentTimer.AddSeconds(MaxTimer - Mathf.RoundToInt(value)):mm:ss}";
                }
                else
                {
                    TimerIco.color = Color.red;
                    TimerText.text = $"-{currentTimer.AddSeconds(Mathf.RoundToInt(value) - MaxTimer):mm:ss}";
                }
            }
        }


        public float MaxBag { get; set; }

        public float CurrentBag
        {
            set
            {
                if (value < MaxBag)
                {
                    BagIco.color = Color.white;
                }
                else
                {
                    BagIco.color = Color.green;
                }

                BagText.text = $"{value}/{MaxBag}";
            }
        }

        public int MaxEnemy
        {
            get => _maxEnemy;
            set
            {
                _maxEnemy = value;
                EnemyText.text = $"{CurrentEnemy}/{MaxEnemy}";
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
                    EnemyIco.color = Color.white;
                }
                else
                {
                    EnemyIco.color = Color.green;
                }

                EnemyText.text = $"{value}/{MaxEnemy}";
            }
        }


        public int MaxBandit
        {
            private get { return _maxBandit; }
            set
            {
                if (value == 0)
                {
                    BanditIco.enabled = false;
                    BanditText.enabled = false;
                }
                else
                {
                    BanditIco.enabled = true;
                    BanditText.enabled = true;
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
                    BanditIco.color = Color.white;
                }
                else
                {
                    BanditIco.color = Color.green;
                }

                BanditText.text = $"{value}/{MaxBandit}";
            }
        }


        public void SetModels(BattleProgressModel battleModel, BattlePlayerModel playerModel)
        {
            _playerModel = playerModel;
            _battleModel = battleModel;

            _playerModel.ChangeMaxHp += f => { MaxHpValue = f; };
            _playerModel.ChangeCurrentHp += f => { CurrentHpValue = f; };

            _battleModel.ChangeMaxTimer += f => { MaxTimer = f; };
            _battleModel.ChangeCurrentTimer += f => { CurrentTimer = f; };

            _battleModel.ChangeMaxEnemy += f => { MaxEnemy = f; };
            _battleModel.ChangeCurrentEnemy += f => { CurrentEnemy = f; };

            _battleModel.ChangeMaxBag += f => { MaxBag = f; };
            _battleModel.ChangeCurrentBag += f => { CurrentBag = f; };

            _battleModel.ChangeMaxBandit += f => { MaxBandit = f; };
            _battleModel.ChangeCurrentBandit += f => { CurrentBandit = f; };
        }
    }
}