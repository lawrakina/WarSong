using EcsBattle.Components;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.PlayerMove
{
    public class MovementPlayer1SetDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent> _player;
        private EcsFilter<InputControlComponent> _input;

        public void Run()
        {
            foreach (var i in _input)
            {
                foreach (var p in _player)
                {
                    _player.GetEntity(p).Get<DirectionMoving>().Value =
                        Vector3.ClampMagnitude(_input.Get1(i).LastPosition, 1f);
                }
            }
        }
    }
}