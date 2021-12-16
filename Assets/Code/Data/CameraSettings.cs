using System;
using System.Collections;
using System.Collections.Generic;
using Guirao.UltimateTextDamage;
using UnityEngine;


namespace Code.Data{
    // [Serializable]
    [CreateAssetMenu(fileName = nameof(CameraSettings), menuName = "Configs/" + nameof(CameraSettings))]
    public class CameraSettings : ScriptableObject{
        [SerializeField]
        private UltimateTextDamageManager _textDamageManager;
        [SerializeField]
        private Vector3 _offsetForDamageManager = new Vector3(0, 2f, 0);
        public Camera CameraPrefab;
        public Vector3 CameraStartPosition = new Vector3(0f, 3.57f, 9.15f);
        public Vector3 CameraStartRotation = new Vector3(21.1f, -180f, 0);
        //We need Shader render mode - Fade
        public FadeObstructionsManager FaderManager;
        [SerializeField]
        public List<CameraAngle> CameraAngles;
        public UltimateTextDamageManager TextDamageManager => _textDamageManager;
        public Vector3 OffsetForDamageText => _offsetForDamageManager;
    }

    [Serializable]
    public class CameraAngle{
        public CameraAngles Angle;
        public Vector3 Position;
        public Vector3 Rotation;
    }
}