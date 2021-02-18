using EcsBattle.Components;
using Leopotam.Ecs;
using Models;


namespace EcsBattle.Systems.Ui
{
    public class UpdatePlayerMaxHealthPointsSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, NeedUpdateMaxHpFromPlayerComponent> _newMaxHpValue;
        private BattlePlayerModel _playerModel;
        public void Run()
        {
            foreach (var index in _newMaxHpValue)
            {
                _playerModel.MaxHp = _newMaxHpValue.Get2(index).Value;
            }
        }
    }
}