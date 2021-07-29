using EcsBattle.Components;
using Leopotam.Ecs;
using Unit.Player;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    public sealed class CreatePoolOfAmmunitionForRangeWeaponSystem : IEcsInitSystem
    {
        //https://docs.google.com/document/d/14j7TUxG4Bvi9p5FsyRqKQ0m_KTsO8IFxL0Kvd_FrIok/edit#
        private readonly int _capacity;
        private IPlayerView _player;
        private EcsWorld _world;
        private EcsFilter<PlayerComponent, BattleInfoMainWeaponComponent> _filter;

        public CreatePoolOfAmmunitionForRangeWeaponSystem(int capacity)
        {
            _capacity = capacity;
        }

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var battleInfo = ref _filter.Get2(i);

                for (var j = 0; j < _capacity; j++)
                {
                    var go = Object.Instantiate(battleInfo._bullet, _player.Transform, true);
                    go.name = battleInfo._bullet.name;
                    go.gameObject.SetActive(false);
                    var entityBullet = _world.NewEntity();
                    entityBullet.Get<WeaponBulletComponent>()._value = go;
                    entityBullet.Get<DisableComponent>();
                }
            }
        }
    }
}