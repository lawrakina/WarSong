using System;
using Data;
using Unit.Player;


namespace Controller
{
    [Serializable]
    public class ListCharacterModel
    {
        public PlayerData _playerData;
        public IPlayerFactory _playerFactory;
        private PlayerView _player;

        public PlayerView Player
        {
            get => _player;
            set=> _player = value;
        }

        public ListCharacterModel(PlayerData playerData, IPlayerFactory playerFactory, PlayerView player)
        {
            _playerData = playerData;
            _playerFactory = playerFactory;
            Player = player;
        }
    }
}