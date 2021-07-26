using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Unit;
using Code.Extension;
using Code.Profile;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.UI.CharacterList
{
    public class CharacterPrototypeController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        private CreatePrototypeView _createPrototypeView;
        private List<PresetCharacterSettings> _listPresetCharacter;
        private PresetCharacterSettings _currentPresetSetting;

        private int _position;

        private EditPrototypeView _editPrototypeView;

        public Action<CharacterSettings> OnPrototypeChange;
        public Action OnCreateCharacter;
        private bool _init = false;
        private DataSettings _settings;


        #region Properties

        private PresetCharacterSettings CurrentPresetSetting
        {
            get
            {
                if (_position == -1 || _position >= _listPresetCharacter.Count)
                    throw new IndexOutOfRangeException(
                        $"CharacterPrototypeController.CurrentPresetSetting._position:{_position}._listPresetCharacter.Count:{_listPresetCharacter.Count}");
                return _currentPresetSetting;
            }
            set
            {
                _currentPresetSetting?.UiBadge?.SetActive(false);
                _currentPresetSetting = value;
                _currentPresetSetting.UiBadge.SetActive(true);
                OnPrototypeChange?.Invoke(value.GetBase());
            }
        }

        #endregion


        #region Class live cycles

        public CharacterPrototypeController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _settings = profilePlayer.Settings;

            //Change class
            _createPrototypeView = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.Create_CreatePrototype,
                _placeForUi, false);
            AddGameObjects(_createPrototypeView.gameObject);
            _listPresetCharacter = new List<PresetCharacterSettings>();
            foreach (var presetsSetting in _settings.PlayerClassesData._presetCharacters.listPresetsSettings)
            {
                var badgeGo = Object.Instantiate(presetsSetting.UiBadge, _createPrototypeView._placeForBadge, false);
                var item = (PresetCharacterSettings) presetsSetting.Clone();
                item.UiBadge = badgeGo;
                _listPresetCharacter.Add(item);
                AddGameObjects(badgeGo);
                if (_listPresetCharacter.Count == 1)
                {
                    CurrentPresetSetting = item;
                    _position = 0;
                    item.UiBadge.SetActive(true);
                }
                else
                {
                    item.UiBadge.SetActive(false);
                }
            }

            _createPrototypeView.Init(MovePrev, MoveNext, GoToSettingChar);

            //Change race, gander,...
            _editPrototypeView = ResourceLoader.InstantiateObject(
                _profilePlayer.Settings.UiViews.Create_EditProtype,
                _placeForUi, false);
            AddGameObjects(_editPrototypeView.gameObject);
            _editPrototypeView.Init(ChangeGender, ChangeRace, BackToSelectClass, CreateCharacter);

            _createPrototypeView.Hide();
            _editPrototypeView.Hide();

            OffExecute();
        }

        #endregion


        #region Methods

        public void Init()
        {
            _init = true;
            base.OnExecute();
            _createPrototypeView.Show();
        }

        private void GoToSettingChar()
        {
            _createPrototypeView.Hide();
            _editPrototypeView.Show();
        }

        private void ChangeGender(CharacterGender gender)
        {
            CurrentPresetSetting.CharacterGender = gender;
            OnPrototypeChange?.Invoke(CurrentPresetSetting.GetBase());
        }

        private void ChangeRace(CharacterRace race)
        {
            CurrentPresetSetting.CharacterRace = race;
            OnPrototypeChange?.Invoke(CurrentPresetSetting.GetBase());
        }

        private void BackToSelectClass()
        {
            _editPrototypeView.Hide();
            _createPrototypeView.Show();
        }

        private void CreateCharacter()
        {
            _editPrototypeView.Hide();
            OnPrototypeChange?.Invoke(CurrentPresetSetting.GetBase());
            _settings.PlayerData.ListCharacters.Add(CurrentPresetSetting);
            _settings.PlayerData._numberActiveCharacter =
                _settings.PlayerData.ListCharacters.Count - 1;
            OnCreateCharacter?.Invoke();
        }


        private void MovePrev()
        {
            if (_position > 0)
            {
                _position--;
                CurrentPresetSetting = _listPresetCharacter[_position];
            }
        }

        private void MoveNext()
        {
            if (_position < _listPresetCharacter.Count - 1)
            {
                _position++;
                CurrentPresetSetting = _listPresetCharacter[_position];
            }
        }

        protected override void OnDispose()
        {
            OnPrototypeChange = null;
            _listPresetCharacter?.Clear();
            base.OnDispose();
        }

        #endregion

        public void CreateNewPrototype()
        {
            if (!_init)
                Init();
            _currentPresetSetting = _settings.PlayerClassesData._presetCharacters.listPresetsSettings
                .FirstOrDefault();
            CreatePlayerFromFabric();
        }

        private void CreatePlayerFromFabric()
        {
            if (_profilePlayer.CurrentPlayer == null)
            {
                var prototype = _profilePlayer.CharacterFabric.CreatePlayer(_currentPresetSetting);
                _profilePlayer.CurrentPlayer = prototype;
                OnPrototypeChange?.Invoke(CurrentPresetSetting.GetBase());    
            }
            else
            {
                _profilePlayer.RebuildCharacter(CurrentPresetSetting);
            }
        }
    }
}