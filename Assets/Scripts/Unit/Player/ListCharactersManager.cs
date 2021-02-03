using System;
using System.Collections.Generic;
using Data;
using Extension;
using UniRx;


namespace Unit.Player
{
    // public sealed class ListCharactersManager
    // {
    //     #region Fields
    //
    //     private readonly PlayerData _playerData;
    //     private IPlayerFactory _playerFactory;
    //     private readonly List<IPlayerView> _characters;
    //
    //     private readonly ReactiveProperty<IPlayerView> _currentChar;
    //     private CustomizingCharacter _customizingCharacter;
    //
    //     #endregion
    //
    //
    //     #region Properties
    //
    //     public ReactiveProperty<IPlayerView> CurrentChar
    //     {
    //         get
    //         {
    //             if (Position == -1 || Position >= _characters.Count)
    //                 throw new InvalidOperationException("fuck off");
    //             return _currentChar;
    //         }
    //     }
    //
    //     private int Position
    //     {
    //         get => _playerData._numberActiveCharacter;
    //         set => _playerData._numberActiveCharacter = value;
    //     }
    //
    //     public PrototypePlayerModel PrototypePlayer { get; }
    //
    //     #endregion
    //
    //
    //     #region ClassLiveCycles
    //
    //     public ListCharactersManager(IPlayerFactory playerFactory, PlayerData playerData)
    //     {
    //         _playerFactory = playerFactory;
    //         _playerData = playerData;
    //         _characters = new List<IPlayerView>();
    //         foreach (var characterSettings in _playerData._characters.ListCharacters)
    //         {
    //             // _characters.Add(_playerFactory.CreatePlayer(_playerData.StoragePlayerPrefab, characterSettings));
    //         }
    //
    //         _currentChar = new ReactiveProperty<IPlayerView>();
    //         if (_characters.Count > 0) CurrentChar.Value = _characters[Position];
    //
    //         PrototypePlayer = new PrototypePlayerModel();
    //     }
    //
    //     public ListCharactersManager(PlayerFactory playerFactory, CustomizingCharacter customizingCharacter,
    //         PlayerData playerData)
    //     {
    //         _playerFactory = playerFactory;
    //         _playerData = playerData;
    //         _customizingCharacter = customizingCharacter;
    //         
    //         _characters = new List<IPlayerView>();
    //         foreach (var characterSettings in _playerData._characters.ListCharacters)
    //         {
    //             _characters.Add(_playerFactory.CreatePlayer(_customizingCharacter..StoragePlayerPrefab, characterSettings));
    //         }
    //
    //         _currentChar = new ReactiveProperty<IPlayerView>();
    //         if (_characters.Count > 0) CurrentChar.Value = _characters[Position];
    //     }
    //
    //     #endregion
    //
    //
    //     #region Methods
    //
    //     public bool MoveNext()
    //     {
    //         if (Position < _characters.Count - 1)
    //         {
    //             Position++;
    //             CurrentChar.Value = _characters[Position];
    //             Dbg.Log($"ListCharactersManager.MoveNext.Position:{Position}");
    //             return true;
    //         }
    //
    //         return false;
    //     }
    //
    //     public bool MovePrev()
    //     {
    //         if (Position > 0)
    //         {
    //             Position--;
    //             CurrentChar.Value = _characters[Position];
    //             Dbg.Log($"ListCharactersManager.MovePrev.Position:{Position}");
    //             return true;
    //         }
    //
    //         return false;
    //     }
    //
    //     public void SaveNewCharacter()
    //     {
    //         // var newPlayer = _playerFactory.CreatePlayer(_playerData.StoragePlayerPrefab, PrototypePlayer.GetCharacterSettings);
    //         // _characters.Add(newPlayer);
    //         // //сохранили в ScripObj
    //         // _playerData._characters.ListCharacters.Add(PrototypePlayer.GetCharacterSettings);
    //         // Position = _characters.Count - 1;
    //         // CurrentChar.Value = _characters[Position];
    //     }
    //
    //     #endregion
    // }
}