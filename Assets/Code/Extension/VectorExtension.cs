using System;
using Code.Data.Unit;
using UnityEngine;


namespace Code.Extension{
    public static class VectorExtension{
        public static Vector3 Change(this Vector3 org, object x = null, object y = null, object z = null){
            return new Vector3(
                x == null ? org.x : (float) x,
                y == null ? org.y : (float) y,
                z == null ? org.z : (float) z);
        }

        public static bool CheckBlocked(Transform player, Transform target, Vector3 offset){
            if (!Physics.Linecast(player.position + offset, target.position + offset, out var hit))
                return true;
            return hit.transform != target;
        }

        public static Vector2 ArithOperation(this Vector2 vector, ModificationOfObjectOfParam modificator){
            return modificator.TypeOfOperation switch{
                ArithmeticOperation.Addition => new Vector2(vector.x + modificator.Value, vector.y + modificator.Value),
                ArithmeticOperation.Multiplication => new Vector2(vector.x * modificator.Value,
                    vector.y * modificator.Value),
                _ => Vector2.zero
            };
        }
    }
}