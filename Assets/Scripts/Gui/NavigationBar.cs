using System;
using System.Collections.Generic;
using Enums;
using Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Gui
{
    [Serializable] public sealed class NavigationBar : MonoBehaviour, IInit, ICleanup
    {
        private IReactiveProperty<EnumMainWindow> _activeWindow;
        private IReactiveProperty<EnumBattleWindow> _battleState;
        private CompositeDisposable _subscriptions;
        public Toggle BattleToggle;

        public Toggle CharToggle;
        public Toggle EquipToggle;
        public Toggle SpellsToggle;
        public Toggle TalentsToggle;

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }


        public void Init()
        {
        }

        public void Init(List<EnumMainWindow> offItemMenu)
        {
            Init();
            Debug.Log($"offItemMenu:{offItemMenu},{offItemMenu.Count}");
            CharToggle.interactable = !offItemMenu.Contains(EnumMainWindow.Character);
            EquipToggle.interactable = !offItemMenu.Contains(EnumMainWindow.Equip);
            BattleToggle.interactable = !offItemMenu.Contains(EnumMainWindow.Battle);
            SpellsToggle.interactable = !offItemMenu.Contains(EnumMainWindow.Spells);
            TalentsToggle.interactable = !offItemMenu.Contains(EnumMainWindow.Talents);
        }

        public void Ctor(IReactiveProperty<EnumMainWindow> activeWindow,
            IReactiveProperty<EnumBattleWindow> battleState)
        {
            _subscriptions = new CompositeDisposable();
            _battleState = battleState;
            _activeWindow = activeWindow;

            _activeWindow.Subscribe(active =>
            {
                switch (active)
                {
                    case EnumMainWindow.None:
                        break;

                    case EnumMainWindow.Character:
                        CharToggle.isOn = true;
                        break;

                    case EnumMainWindow.Equip:
                        EquipToggle.isOn = true;
                        break;

                    case EnumMainWindow.Battle:
                        BattleToggle.isOn = true;
                        break;

                    case EnumMainWindow.Spells:
                        SpellsToggle.isOn = true;
                        break;

                    case EnumMainWindow.Talents:
                        TalentsToggle.isOn = true;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(active), active, null);
                }
            });

            CharToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x)
                    // Debug.Log($"_activeWindow.Value = EnumMainWindow.Character");
                    _activeWindow.Value = EnumMainWindow.Character;
            }).AddTo(_subscriptions);
            EquipToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x)
                    // Debug.Log($"_activeWindow.Value = EnumMainWindow.Equip");
                    _activeWindow.Value = EnumMainWindow.Equip;
            }).AddTo(_subscriptions);
            BattleToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x)
                {
                    // Debug.Log($"_activeWindow.Value = EnumMainWindow.Battle");
                    _activeWindow.Value = EnumMainWindow.Battle;
                    _battleState.Value = EnumBattleWindow.DungeonGenerator;
                }
            }).AddTo(_subscriptions);
            SpellsToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x)
                    // Debug.Log($"_activeWindow.Value = EnumMainWindow.Spells");
                    _activeWindow.Value = EnumMainWindow.Spells;
            }).AddTo(_subscriptions);
            TalentsToggle.OnValueChangedAsObservable().Subscribe(x =>
            {
                if (x)
                    // Debug.Log($"_activeWindow.Value = EnumMainWindow.Talents");
                    _activeWindow.Value = EnumMainWindow.Talents;
            }).AddTo(_subscriptions);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}