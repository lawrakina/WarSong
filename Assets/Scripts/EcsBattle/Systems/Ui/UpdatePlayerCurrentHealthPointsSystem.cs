using EcsBattle.Components;
using Leopotam.Ecs;
using Models;


namespace EcsBattle.Systems.Ui
{
    public class UpdatePlayerCurrentHealthPointsSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, NeedUpdateCurrentHpFromPlayerComponent> _newCurrentHpValue;
        private BattlePlayerModel _playerModel;
        public void Run()
        {
            foreach (var index in _newCurrentHpValue)
            {
                _playerModel.CurrentHp = _newCurrentHpValue.Get2(index).Value;
            }
        }
    }
}