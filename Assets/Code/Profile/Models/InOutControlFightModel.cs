using System;
using Code.Unit;
using UnityEngine;


namespace Code.Profile.Models
{
    [CreateAssetMenu(fileName = nameof(InOutControlFightModel), menuName = "Models/" + nameof(InOutControlFightModel))]
    public class InOutControlFightModel : ScriptableObject
    {
        [SerializeField]
        private BattleProgressModel _battleProgress = new BattleProgressModel();

        [SerializeField]
        private InputControlModel _inpuControlModel = new InputControlModel();

        [SerializeField]
        private PlayerStatsModel _playerStatsModel = new PlayerStatsModel();

        [SerializeField]
        private PlayerTargetModel _playerTargetModel = new PlayerTargetModel();

        public PlayerStatsModel PlayerStats => _playerStatsModel;
        public BattleProgressModel BattleProgress => _battleProgress;
        public InputControlModel InputControl => _inpuControlModel;
        public PlayerTargetModel PlayerTarget => _playerTargetModel;
    }
}