using System;
using System.Collections.Generic;
using Extension;
using Interface;
using Unit.Player;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Controller
{
    public sealed class ListOfCharactersController: IInitialization
    {
        #region Fields

        private readonly ListCharacterModel _model;
        private readonly ListCharacterView _view;
        private readonly List<PlayerView> _characters;
        private PlayerView _currentChar;

        public Action<PlayerView> ChangePlayer;

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
                _currentChar = value;
                ChangePlayer?.Invoke(value);
            }
        }
        
        private int Position
        {
            get => _model._playerData._numberActiveCharacter;
            set => _model._playerData._numberActiveCharacter = value;
        }

        #endregion


        #region ClassLiveCycles

        public ListOfCharactersController(ListCharacterModel model, ListCharacterView view)
        {
            _model = model;
            _view = view;
            
            _characters = new List<PlayerView>();
            for (var index = 0; index < _model._playerData._characters.ListCharacters.Count; index++)
            {
                var item = _model._playerData._characters.ListCharacters[index];
                var character = (PlayerView) _model._playerFactory.CreatePlayer(item);
                character.Transform.position = new Vector3(index * 3,0.0f,0.0f);
                _characters.Add(character);
            }

            view.CharacterWindow._listCharacterPanel._moveNextCharAction.OnAction += () => MoveNext();
            view.CharacterWindow._listCharacterPanel._movePrevCharAction.OnAction += () => MovePrev();
            view.CharacterWindow._listCharacterPanel._selectCharAction.OnAction += () =>
            {
                Dbg.Log($"SelectButtonClick.Char:{_characters[_model._playerData._numberActiveCharacter]}");
                CurrentCharacter = _characters[_model._playerData._numberActiveCharacter];
            };
            
            ChangePlayer += value =>
            {
                GoToSumpTank(_model.Player);
                _model.Player = value;
                GoToActivePosition(_model.Player, _view.CharacterWindow.GetPositionCharacter());
            };
            ChangePlayer += value =>
            {
                _view.CharacterWindow._listCharacterPanel._info.text = GetInfoByPlayer(_model.Player);
            };
            
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

        public void Initialization()
        {
            CurrentCharacter = _characters[_model._playerData._numberActiveCharacter];
        }
        
        ~ListOfCharactersController()
        {
            ChangePlayer = null;
        }

        #endregion


        #region Methods

        private bool MoveNext()
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

        private bool MovePrev()
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

        private static void GoToActivePosition(PlayerView player, Transform newPosition)
        {
            player.gameObject.SetActive(true);
            player.Transform.position = newPosition.position;
            player.Transform.rotation = newPosition.rotation;
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