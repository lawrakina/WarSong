using System;
using System.Collections.Generic;
using Code.Fight;
using Guirao.UltimateTextDamage;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Code.Data
{
    // [Serializable]
    [CreateAssetMenu(fileName = nameof(CameraSettings), menuName = "Configs/" + nameof(CameraSettings))]
    public class CameraSettings : ScriptableObject
    {
        public Camera CameraPrefab;
        public Vector3 CameraStartPosition = new Vector3(0f, 3.57f, 9.15f);
        public Vector3 CameraStartRotation = new Vector3(21.1f, -180f, 0);
        public UltimateTextDamageManager _textDamageManager;
        public FadeObstructionsManager FaderManager;
        //We need Shader render mode - Fade
    }
}