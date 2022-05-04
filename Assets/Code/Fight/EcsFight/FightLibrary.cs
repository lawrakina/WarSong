using Code.Data.Abilities;
using Code.Fight.EcsFight.Battle;
using Code.Unit;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.Fight.EcsFight{
    public static class FightLibrary{
        public static void SingleMomentumAttack<T>(GameObject target, Weapon<T> weapon, EcsEntity unit){
            var targetCollision = target.transform.GetComponent<ICollision>();
            var collision =
                new InfoCollision(weapon.Value.GetDamage(), unit);
            targetCollision?.OnCollision(collision);
        }

        // public static void SingleMomentumAttack<T>(GameObject target, Weapon<T> weapon, EcsEntity entity){
            
        // }
        public static void SingleMomentumAttackWithModifierFromAbility<T>(GameObject target, Weapon<T> weapon,
            EcsEntity entity, TemplateAbility ability){
            var targetCollision = target.transform.GetComponent<ICollision>();
            var collision =
                new InfoCollision(weapon.Value.GetDamage(modifier:ability.value), entity);
            targetCollision?.OnCollision(collision);
            // target.Value.Health.CurrentHp -= ability.value * weapon.Value.GetDamage(modifier).Damage;
        }
    }

    // public abstract class T{
    // }
}