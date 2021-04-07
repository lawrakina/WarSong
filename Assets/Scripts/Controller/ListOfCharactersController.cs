using System;
using System.Collections.Generic;
using System.Linq;
using Controller.Model;
using Data;
using Enums;
using Extension;
using Interface;
using UniRx;
using Unit.Player;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Controller
{
    public sealed class ListOfCharactersController : IInitialization
    {
        #region Fields

        private readonly ListCharacterModel _model;
        private readonly CommandManager _commandManager;
        private readonly List<PlayerView> _characters;
        private IPlayerView _currentChar;
        private CharacterSettings _presetCharacters;

        private readonly CompositeDisposable _subscriptions;

        #endregion


        #region Properties

        private IPlayerView CurrentCharacter
        {
            get
            {
                if (Position == -1 || Position >= _characters.Count)
                    throw new InvalidOperationException("fuck off");
                return _currentChar;
            }
            set
            {
                GoToSumpTank(_currentChar);
                _currentChar = value;
                _commandManager.ChangePlayer.ForceExecute(value);
                _currentChar.Transform.gameObject.SetActive(true);
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
                character.Transform.position = new Vector3(index * 3, 0.0f, 0.0f);
                _characters.Add(character);
            }

            CurrentCharacter = _characters[_model._playerData._numberActiveCharacter];
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

        public void CreateNewPrototype()
        {
            GoToSumpTank(CurrentCharacter);
            _presetCharacters = _model._playerData._presetCharacters.listPresetsSettings.FirstOrDefault();
            CreatePlayerFromFabric();
        }

        public void UpdatePrototype(
            object characterClass = null, 
            object characterGender = null,
            object characterRace = null)
        {
            if (characterClass != null)
            {
                _presetCharacters =
                    _model._playerData._presetCharacters.listPresetsSettings.FirstOrDefault(x =>
                        x.CharacterClass == (CharacterClass)characterClass);
            }
            
            _presetCharacters.CharacterGender = characterGender == null
                ? _presetCharacters.CharacterGender
                : (CharacterGender) characterGender;
            
            _presetCharacters.CharacterRace = characterRace == null
                ? _presetCharacters.CharacterRace
                : (CharacterRace) characterRace;
            
            CreatePlayerFromFabric();
        }

        private void CreatePlayerFromFabric()
        {
            //todo it`s method generate trash!!! Max task => ReUse Prototype character, Min task => destroy all children objects in old CurrentCharacter
            Object.Destroy(CurrentCharacter.Transform.gameObject);
            var prototype = _model._playerFactory.CreatePlayer(_presetCharacters);
            prototype.Transform.gameObject.name =
                $"PROTOTYPE.Character.{_presetCharacters.CharacterClass}.{_presetCharacters.CharacterGender}.{_presetCharacters.CharacterRace}";
            CurrentCharacter = prototype;
        }
        
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

        private static void GoToSumpTank(IPlayerView player)
        {
            if (player == null) return;
            var sumpTank = new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f));
            player.Transform.position = sumpTank;
            player.Transform.gameObject.SetActive(false);
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


        public void CreateCharacterFromPrototype()
        {
            _model._playerData._characters.ListCharacters.Add(_presetCharacters);
            _model._playerData._numberActiveCharacter = _model._playerData._characters.ListCharacters.Count - 1;
        }
    }
}