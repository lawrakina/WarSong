using System;
using UnityEngine;


namespace Data
{
    [Serializable] [CreateAssetMenu(fileName = "CameraSettingsInBattle", menuName = "Data/Camera settings in Battle")]
    public class CameraSettingsInBattle : ScriptableObject
    {
        [SerializeField]
        public float maxTimeToStopFollowingInPlayer = 1.0f;

        [SerializeField]
        public float maxTimeToLerpInPlayer = 1.0f;
    }
}