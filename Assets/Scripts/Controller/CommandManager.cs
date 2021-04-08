using System;
using System.Collections;
using Windows;
using Battle;
using Extension;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;


namespace Controller
{
    public sealed class CommandManager : IInitialization, ICleanup
    {
        private readonly UiWindows _uiWindows;
        private readonly SceneWindows _windows;
        public readonly ReactiveCommand<bool> _battleWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> _characterWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> _tavernWindowShowCommand = new ReactiveCommand<bool>();
        public readonly ReactiveCommand<bool> _shopWindowShowCommand = new ReactiveCommand<bool>();

        public readonly ReactiveCommand _buildDungeonCommand = new ReactiveCommand();

        public readonly ReactiveCommand<IPlayerView> ChangePlayer = new ReactiveCommand<IPlayerView>();

        private readonly CompositeDisposable _subscriptions;


        public ListOfCharactersController ListOfCharacters { get; set; }
        public GeneratorDungeon GeneratorDungeon { get; set; }
        public EcsBattleInitialization BattleInitialisation { get; set; }

        public CommandManager(UiWindows uiWindows, SceneWindows windows)
        {
            _subscriptions = new CompositeDisposable();
            _uiWindows = uiWindows;
            _windows = windows;
        }

        public void Init()
        {
            #region NavigationPanel

            _battleWindowShowCommand.Subscribe(value =>
            {
                _uiWindows.BattleUiWindow.SetActive(value);
                _uiWindows.CharacterWindow.SetActive(!value);
                _uiWindows.TavernUiWindow.SetActive(!value);
                _uiWindows.ShopUiWindow.SetActive(!value);
                SetActiveRootBattle(value);
                SetActiveRootCharacter(!value);
            }).AddTo(_subscriptions);
            _characterWindowShowCommand.Subscribe(value =>
            {
                _uiWindows.BattleUiWindow.SetActive(!value);
                _uiWindows.CharacterWindow.SetActive(value);
                _uiWindows.TavernUiWindow.SetActive(!value);
                _uiWindows.ShopUiWindow.SetActive(!value);
                SetActiveRootCharacter(value);
                SetActiveRootBattle(!value);
            }).AddTo(_subscriptions);
            _tavernWindowShowCommand.Subscribe(value =>
            {
                _uiWindows.BattleUiWindow.SetActive(!value);
                _uiWindows.CharacterWindow.SetActive(!value);
                _uiWindows.TavernUiWindow.SetActive(value);
                _uiWindows.ShopUiWindow.SetActive(!value);
            }).AddTo(_subscriptions);
            _shopWindowShowCommand.Subscribe(value =>
            {
                _uiWindows.BattleUiWindow.SetActive(!value);
                _uiWindows.CharacterWindow.SetActive(!value);
                _uiWindows.TavernUiWindow.SetActive(!value);
                _uiWindows.ShopUiWindow.SetActive(value);
            }).AddTo(_subscriptions);

            _uiWindows.BottomNavigationWindow._battleToggle.onValueChanged.AddListener(
                (_) => { _battleWindowShowCommand.Execute(true); });
            _uiWindows.BottomNavigationWindow._charToggle.onValueChanged.AddListener(
                (_) => { _characterWindowShowCommand.Execute(true); });
            _uiWindows.BottomNavigationWindow._tavernToggle.onValueChanged.AddListener(
                (_) => { _tavernWindowShowCommand.Execute(true); });
            _uiWindows.BottomNavigationWindow._shopToggle.onValueChanged.AddListener(
                (_) => { _shopWindowShowCommand.Execute(true); });

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
                $"{p.CharacterClass.Name}, Level:{p.UnitLevel.CurrentLevel}, Hp:{p.BaseHp}\n" +
                $"Weapon:{p.UnitPlayerBattle.MainWeapon.name}\n" +
                $"Description:{p.CharacterClass.Description}";
            return result;
        }

        public void Cleanup()
        {
            _subscriptions?.Dispose();
        }
    }
}