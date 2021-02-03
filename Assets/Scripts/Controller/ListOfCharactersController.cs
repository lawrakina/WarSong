using System;
using System.Collections.Generic;
using Data;
using Extension;
using UniRx;
using Unit.Player;


namespace Controller
{
    public sealed class ListOfCharactersController: BaseController
    {
        private PlayerData _playerData;
        private IPlayerFactory _playerFactory;
        
        private readonly List<IPlayerView> _characters;
        private readonly ReactiveProperty<IPlayerView> _currentChar;

        public ReactiveProperty<IPlayerView> CurrentCharacter
        {
            get
            {
                if (Position == -1 || Position >= _characters.Count)
                    throw new InvalidOperationException("fuck off");
                return _currentChar;
            }
        }
        
        private int Position
        {
            get => _playerData._numberActiveCharacter;
            set => _playerData._numberActiveCharacter = value;
        }
        
        public ListOfCharactersController(PlayerData playerData, IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _playerData = playerData;
            
            _characters = new List<IPlayerView>();
            foreach (var item in playerData._characters.ListCharacters)
            {
                var character = _playerFactory.CreatePlayer(item);
                _characters.Add(character);
            }
            _currentChar = new ReactiveProperty<IPlayerView>(_characters[playerData._numberActiveCharacter]);
        }
        
        #region Methods

        public bool MoveNext()
        {
            if (Position < _characters.Count - 1)
            {
                Position++;
                CurrentCharacter.Value = _characters[Position];
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
                CurrentCharacter.Value = _characters[Position];
                Dbg.Log($"ListCharactersManager.MovePrev.Position:{Position}");
                return true;
            }

            return false;
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