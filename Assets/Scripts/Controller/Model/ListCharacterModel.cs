using System;
using Data;
using Unit.Player;


namespace Controller.Model
{
    [Serializable]
    public class ListCharacterModel
    {
        public PlayerData _playerData;
        public IPlayerFactory _playerFactory;

        public ListCharacterModel(PlayerData playerData, IPlayerFactory playerFactory)
        {
            _playerData = playerData;
            _playerFactory = playerFactory;
        }
    }
}