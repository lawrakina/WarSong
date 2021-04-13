using EcsBattle.Components;
using Extension;
using Leopotam.Ecs;
using UnityEngine;


namespace EcsBattle.Systems.Attacks
{
    // public sealed class Attack5StartAnimationStrikeSystem : IEcsRunSystem
    // {
    //     private EcsFilter<NeedStartAnimationAttackComponent, UnitComponent,BattleInfoComponent> _player;
    //     
    //     public void Run()
    //     {
    //         foreach (var i in _player)
    //         {
    //             ref var entity = ref _player.GetEntity(i);
    //             ref var animator = ref _player.Get2(i).animator;
    //             ref var battle = ref _player.Get3(i);
    //
    //             Dbg.Log($"StartAnimationStrikeForUnitsSystem.battle.AnimatorStrikeMaxValue:{battle.AnimatorStrikeMaxValue}");
    //             animator.AttackType = Random.Range(0, battle.AnimatorStrikeMaxValue);
    //             animator.SetTriggerAttack();
    //             entity.Del<NeedStartAnimationAttackComponent>();
    //         }
    //     }
    // }
}