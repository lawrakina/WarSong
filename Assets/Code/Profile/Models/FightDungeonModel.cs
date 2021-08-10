using System;
using Code.Data.Marker;
using Code.Fight;
using UniRx;
using UnityEngine;


namespace Code.Profile.Models
{
    [CreateAssetMenu(fileName = nameof(FightDungeonModel), menuName = "Models/" + nameof(FightDungeonModel))]
    public class FightDungeonModel : ScriptableObject
    {
        [SerializeField]
        private ReactiveProperty<FightState> _fightState = new ReactiveProperty<FightState>(Fight.FightState.Default);

        [SerializeField]
        private ReactiveProperty<string> _infoState = new ReactiveProperty<string>($"");

        public ReactiveProperty<string> InfoState => _infoState;
        public ReactiveProperty<FightState> FightState => _fightState;

        public Action<Transform> OnChangePlayerPosition;
        public Action<SpawnMarkerEnemyInDungeon[]> OnChangeEnemiesPositions;
    }
}