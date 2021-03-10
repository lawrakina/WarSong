using System;
using UnityEngine;


namespace Data
{
    [Serializable] [CreateAssetMenu(fileName = "CameraSettingsInBattle", menuName = "Data/Camera settings in Battle")]
    public class CameraSettingsInBattle : ScriptableObject
    {
        [SerializeField]
        [Range(min: 1.0f, max: 10.0f)]
        public float maxTimeToStopFollowingInPlayer = 1.0f;

        [SerializeField]
        [Range(min: 1.0f, max: 10.0f)]
        public float maxTimeToLerpInPlayer = 1.0f;
    }
}