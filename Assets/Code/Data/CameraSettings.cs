using System;
using Guirao.UltimateTextDamage;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Data
{
    // [Serializable]
    [CreateAssetMenu(fileName = "Config_CameraSettings", menuName = "Data/Config Camera settings")]
    public class CameraSettings : ScriptableObject
    {
        // [SerializeField]
        // [Range(min: 1.0f, max: 10.0f)]
        // public float maxTimeToStopFollowingInPlayer = 1.0f;
        //
        // [SerializeField]
        // [Range(min: 1.0f, max: 10.0f)]
        // public float maxTimeToLerpInPlayer = 1.0f;

        [SerializeField] public UltimateTextDamageManager _textDamageManager;

        public Camera CameraPrefab;
        [SerializeField] public Vector3 CameraStartPosition = new Vector3(0f, 3.57f, 9.15f);
        [SerializeField] public Vector3 CameraStartRotation = new Vector3(21.1f, -180f, 0);
    }
}