using UnityEngine;


namespace Code.Extension
{
    public static class LayerManager
    {
        public static LayerMask GroundLayer;
        public static int EnemyLayer;
        public static int PlayerLayer;
        public static int PlayerAttackLayer;
        public static int EnemyAttackLayer;
        public static int EnemyAndPlayerAttackLayer;

        /// <summary>
        ///     Проверка на вхождение слоя layer в маску слоев layerMask
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static bool CheckForComparerLayer(LayerMask layerMask, int layer)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }

        /// <summary>
        ///     Проверка на вхождение слоя collider.gameObject в маску слоев layerMask
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static bool CheckForComparerLayer(LayerMask layerMask, GameObject gameObject)
        {
            return (layerMask.value & (1 << gameObject.layer)) != 0;
        }

        /// <summary>
        ///     Проверка на вхождение слоя collider.gameObject.layer в маску слоев layerMask
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static bool CheckForComparerLayer(LayerMask layerMask, Collider collider)
        {
            return (layerMask.value & (1 << collider.gameObject.layer)) != 0;
        }
    }
}