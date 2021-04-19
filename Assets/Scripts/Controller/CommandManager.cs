﻿using System;
using System.Collections;
using Windows;
using Battle;
using Extension;
using Gui;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public sealed class CommandManager : IInitialization, ICleanup
    {
        #region Fields

        private readonly UiWindows _uiWindows;
        private readonly SceneWindows _windows;
        public readonly ReactiveCommand<bool> _battleWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> _characterWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> _tavernWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> _shopWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand _buildDungeonCommand = new ReactiveCommand();
        public readonly ReactiveCommand<IPlayerView> ChangePlayer = new ReactiveCommand<IPlayerView>();
        private readonly CompositeDisposable _subscriptions;

        #endregion


        #region Properties

        public ListOfCharactersController ListOfCharacters { get; set; }
        public GeneratorDungeon GeneratorDungeon { get; set; }
        public IBattleInit BattleInitialisation { get; set; }

        #endregion


        #region ClassLiveCycles

        public CommandManager(UiWindows uiWindows, SceneWindows windows)
        {
            _subscriptions = new CompositeDisposable();
            _uiWindows = uiWindows;
            _windows = windows;
        }

        #endregion


        #region IInitialization

        public void Init()
        {
            var navigationToggleGroupWindows = new ToggleWindowGroup();
            navigationToggleGroupWindows.Add(_uiWindows.BattleUiWindow);
            navigationToggleGroupWindows.Add(_uiWindows.CharacterWindow);
            navigationToggleGroupWindows.Add(_uiWindows.TavernUiWindow);
            navigationToggleGroupWindows.Add(_uiWindows.ShopUiWindow);
            navigationToggleGroupWindows.Init();
            
            #region NavigationPanel

            _battleWindowShowCommand.Subscribe(value =>
            {
                if(value)
                    _uiWindows.BattleUiWindow.Show();
                SetActiveRootBattle(value);
                SetActiveRootCharacter(!value);
            }).AddTo(_subscriptions);
            _characterWindowShowCommand.Subscribe(value =>
            {
                if(value)
                    _uiWindows.CharacterWindow.Show();
                SetActiveRootCharacter(value);
                SetActiveRootBattle(!value);
            }).AddTo(_subscriptions);
            _tavernWindowShowCommand.Subscribe(value =>
            {
                if(value)
                    _uiWindows.TavernUiWindow.Show();
            }).AddTo(_subscriptions);
            _shopWindowShowCommand.Subscribe(value =>
            {
                if(value)
                    _uiWindows.ShopUiWindow.Show();
            }).AddTo(_subscriptions);

            _uiWindows.BottomNavigationWindow._battleToggle.onValueChanged.AddListener(
                (_) => { _battleWindowShowCommand.Execute(true); });
            _uiWindows.BottomNavigationWindow._charToggle.onValueChanged.AddListener(
                (_) => { _characterWindowShowCommand.Execute(true); });
            _uiWindows.BottomNavigationWindow._tavernToggle.onValueChanged.AddListener(
                (_) => { _tavernWindowShowCommand.Execute(true); });
            _uiWindows.BottomNavigationWindow._shopToggle.onValueChanged.AddListener(
                (_) => { _shopWindowShowCommand.Execute(true); });

            _uiWindows.TopNavigationUiWindow._toListCharacterCommand.Subscribe(_ =>
            {
                _characterWindowShowCommand.Execute(true);
                _uiWindows.CharacterWindow.aboutActiveCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.createSettingsCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.createNewCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.listCharacterUiWindow.Show();
            }).AddTo(_subscriptions);
            
            #endregion


            #region BattlePanel

            _uiWindows.BattleUiWindow._startBattleCommand.Subscribe(_ =>
            {
                _uiWindows.TopNavigationUiWindow.SetActive(false);
                _uiWindows.BottomNavigationWindow.SetActive(false);
                _uiWindows.BattleUiWindow.SetActive(false);
                _uiWindows.LoadUiWindow.SetActive(true);

                GeneratorDungeon.SetRandomSeed();
                GeneratorDungeon.BuildDungeon();

                _buildDungeonCommand.Execute();
            }).AddTo(_subscriptions);

            _buildDungeonCommand.Subscribe(_ =>
            {
                var buildGun = Observable.FromCoroutine(AsyncBuildDungeon).Subscribe().AddTo(_subscriptions);
            });

            #endregion

            

            #region FightPanel

            _uiWindows.FightUiWindow._pauseBattleCommand.Subscribe(_ =>
            {
                Time.timeScale = 0.0f;
                _uiWindows.FightUiWindow.Hide();
                _uiWindows.PauseFightUiWindow.Show();
            }).AddTo(_subscriptions);

            #endregion


            #region PauseFightPanel

            _uiWindows.PauseFightUiWindow._continueFightCommand.Subscribe(_ =>
            {
                Time.timeScale = 1.0f;
                _uiWindows.PauseFightUiWindow.Hide();
                _uiWindows.FightUiWindow.Show();
            }).AddTo(_subscriptions);

            _uiWindows.PauseFightUiWindow._gotoMainMenuCommand.Subscribe(_ =>
            {
                Time.timeScale = 1.0f;
                BattleInitialisation.UnSaveStopBattle();
                _uiWindows.PauseFightUiWindow.Hide();
                _characterWindowShowCommand.Execute(true);
                _uiWindows.BottomNavigationWindow.Show();
                _uiWindows.TopNavigationUiWindow.Show();
            }).AddTo(_subscriptions);

            #endregion

            #region CharacterPanel

            #region ListCharacterPanel

            ChangePlayer.Subscribe(value =>
            {
                _uiWindows.CharacterWindow.listCharacterUiWindow._info.text = GetInfoByPlayer(value);
                _uiWindows.CharacterWindow.aboutActiveCharacterUiWindow._info.text = GetInfoByPlayer(value);
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.listCharacterUiWindow._selectCharCommand.Subscribe(_ =>
            {
                _uiWindows.CharacterWindow.listCharacterUiWindow.SetActive(false);
                _uiWindows.CharacterWindow.aboutActiveCharacterUiWindow.SetActive(true);
                ListOfCharacters.UpdateCurrentCharacter();
            }).AddTo(_subscriptions);
            _uiWindows.CharacterWindow.listCharacterUiWindow._nextCharCommand.Subscribe(_ =>
            {
                ListOfCharacters.MoveNext();
            }).AddTo(_subscriptions);
            _uiWindows.CharacterWindow.listCharacterUiWindow._prevCharCommand.Subscribe(_ =>
            {
                ListOfCharacters.MovePrev();
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.listCharacterUiWindow._createCharCommand.Subscribe(_ =>
            {
                _uiWindows.CharacterWindow.listCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.createNewCharacterUiWindow.Show();
                ListOfCharacters.CreateNewPrototype();
            }).AddTo(_subscriptions);


            #region CreateNewCharacter

            _uiWindows.CharacterWindow.createNewCharacterUiWindow.selectCharacterClassCommand.Subscribe(value =>
            {
                ListOfCharacters.UpdatePrototype(characterClass: value);
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.createNewCharacterUiWindow.gotoSettingsCharacterClassCommand.Subscribe(_ =>
            {
                _uiWindows.CharacterWindow.createNewCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.createSettingsCharacterUiWindow.Show();
                ListOfCharacters.UpdatePrototype();
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.createSettingsCharacterUiWindow._selectGenderCommand.Subscribe(value =>
            {
                ListOfCharacters.UpdatePrototype(characterGender: value);
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.createSettingsCharacterUiWindow._selectRaceCommand.Subscribe(value =>
            {
                ListOfCharacters.UpdatePrototype(characterRace: value);
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.createSettingsCharacterUiWindow._toSelectClassCommand.Subscribe(_ =>
            {
                _uiWindows.CharacterWindow.createSettingsCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.createNewCharacterUiWindow.Show();
                ListOfCharacters.UpdatePrototype();
            }).AddTo(_subscriptions);

            _uiWindows.CharacterWindow.createSettingsCharacterUiWindow._createNewCharacterCommand.Subscribe(_ =>
            {
                ListOfCharacters.CreateCharacterFromPrototype();
                _uiWindows.CharacterWindow.createSettingsCharacterUiWindow.Hide();
                _uiWindows.CharacterWindow.aboutActiveCharacterUiWindow.Show();
            }).AddTo(_subscriptions);

            #endregion

            #endregion

            #endregion
        }

        #endregion


        #region Private Methods

        private void SetActiveRootBattle(bool value)
        {
            _windows.RootBattleCamera.enabled = value;
            _windows.RootBattleWindow.SetActive(value);
        }

        private void SetActiveRootCharacter(bool value)
        {
            _windows.RootCharacterCamera.enabled = value;
            _windows.RootCharacterWindow.SetActive(value);
            _windows.RootCharacterEnvironment.SetActive(value);
        }

        private IEnumerator AsyncBuildDungeon()
        {
            while (true)
            {
                if (GeneratorDungeon.GetPlayerPosition() == null)
                {
                    Dbg.Log($"CheckStatus Dungeon: {GeneratorDungeon.GetPlayerPosition()} ?");
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    yield return new WaitForSeconds(1);
                    Dbg.Log($"Start BATTLE: {GeneratorDungeon.GetPlayerPosition()} ?");
                    BattleInitialisation.StartBattle();
                    _uiWindows.FightUiWindow.Show();
                    _uiWindows.LoadUiWindow.SetActive(false);
                    yield break;
                }
            }
        }

        private static string GetInfoByPlayer(IPlayerView p)
        {
            var result =
                $"{p.CharacterClass.Name}, Level:{p.UnitLevel.CurrentLevel}, Hp:{p.UnitHealth.MaxHp}\n" +
                $"Weapon:{p.UnitPlayerBattle.MainWeapon.name}\n" +
                $"Description:{p.CharacterClass.Description}";
            return result;
        }

        #endregion

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }
    }
}