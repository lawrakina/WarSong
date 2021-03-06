﻿using EcsBattle.Components;
using EcsBattle.Systems.Attacks;
using EcsBattle.Systems.Player;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.PlayerMove
{
    public class MovementPlayer1SetDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<MovementEventComponent, DirectionMovementComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var eventVector = ref _filter.Get1(i);
                ref var direction = ref _filter.Get2(i);

                direction._value.localPosition =  Vector3.ClampMagnitude(eventVector._value, 1f);

                if (eventVector._value.sqrMagnitude > new Vector3(0.2f, 0.2f, 0.2f).sqrMagnitude)
                {
                    entity.Del<NeedMoveToTargetAndAttackComponent>();
                }
                entity.Del<MovementEventComponent>();
            }
        }
    }
}