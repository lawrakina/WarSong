using Guirao.UltimateTextDamage;
using UnityEngine;


namespace EcsBattle.Components
{
    public struct FightCameraComponent
    {
        // public float maxTimeToStopFollowingInPlayer;
        // public float maxTimeToLerpInPlayer;
        // public Transform positionThirdTarget;
        // public Transform positionPlayerTransform;
        public UltimateTextDamageManager uiTextManager;
    }

    public struct NeedLerpPositionCameraFollowingToTargetComponent
    {
        public float _currentTime;
        public float _maxTime;
    }
}