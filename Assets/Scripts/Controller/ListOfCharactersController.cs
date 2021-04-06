using System;
using System.Collections.Generic;
using Controller.Model;
using Extension;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Controller
{
    public sealed class ListOfCharactersController: IInitialization
    {
        #region Fields

        private readonly ListCharacterModel _model;
        private readonly CommandManager _commandManager;
        private readonly List<PlayerView> _characters;
        private PlayerView _currentChar;

        private readonly CompositeDisposable _subscriptions;
        #endregion


        #region Properties

        public PlayerView CurrentCharacter
        {
            get
            {
                if (Position == -1 || Position >= _characters.Count)
                    throw new InvalidOperationException("fuck off");
                return _currentChar;
            }
            private set
            {
                GoToSumpTank(_currentChar);
                _currentChar = value;
                _commandManager.ChangePlayer.ForceExecute(value);
                _currentChar.gameObject.SetActive(true);
            }
        }
        
        private int Position
        {
            get => _model._playerData._numberActiveCharacter;
            set => _model._playerData._numberActiveCharacter = value;
        }

        #endregion


        #region ClassLiveCycles

        public ListOfCharactersController(ListCharacterModel model, CommandManager commandManager)
        {
            _subscriptions = new CompositeDisposable();
            _model = model;
            _commandManager = commandManager;

            _characters = new List<PlayerView>();
            for (var index = 0; index < _model._playerData._characters.ListCharacters.Count; index++)
            {
                var item = _model._playerData._characters.ListCharacters[index];
                var character = (PlayerView) _model._playerFactory.CreatePlayer(item);
                character.Transform.position = new Vector3(index * 3,0.0f,0.0f);
                _characters.Add(character);
            }
            
            CurrentCharacter = _characters[_model._playerData._numberActiveCharacter];
        }

        private static string GetInfoByPlayer(PlayerView p)
        {
            var result =
                $"{p.CharacterClass.Name}, Level:{p.UnitLevel.CurrentLevel}, Hp:{p.BaseHp}\n" +
                $"Weapon:{p.UnitPlayerBattle.Weapon.name}\n" +
                $"Description:{p.CharacterClass.Description}";
            return result;
        }

        public void Init()
        {
            CurrentCharacter = _characters[_model._playerData._numberActiveCharacter];
        }
        
        ~ListOfCharactersController()
        {
            _subscriptions?.Dispose();
        }

        #endregion


        #region Methods

        public void UpdateCurrentCharacter()
        {
            CurrentCharacter = _characters[_model._playerData._numberActiveCharacter];
        }
        
        public bool MoveNext()
        {
            if (Position < _characters.Count - 1)
            {
                Position++;
                CurrentCharacter = _characters[Position];
                Dbg.Log($"ListCharactersManager.MoveNext.Position:{Position}");
                return true;
            }

            return false;
        }

        public bool MovePrev()
        {
            if (Position > 0)
            {
                Position--;
                CurrentCharacter = _characters[Position];
                Dbg.Log($"ListCharactersManager.MovePrev.Position:{Position}");
                return true;
            }

            return false;
        }
        
        private static void GoToSumpTank(PlayerView player)
        {
            if(player == null) return;
            var sumpTank = new Vector3(Random.Range(-2.0f,2.0f), 0.0f, Random.Range(-2.0f,2.0f));
            player.Transform.position = sumpTank;
            player.gameObject.SetActive(false);
        }
        
        public void SaveNewCharacter()
        {
            // var newPlayer = _playerFactory.CreatePlayer(_playerData.StoragePlayerPrefab, PrototypePlayer.GetCharacterSettings);
            // _characters.Add(newPlayer);
            // //сохранили в ScripObj
            // _playerData._characters.ListCharacters.Add(PrototypePlayer.GetCharacterSettings);
            // Position = _characters.Count - 1;
            // CurrentChar.Value = _characters[Position];
        }

        #endregion
    }
}