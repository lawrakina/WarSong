using UnityEngine;


namespace EcsBattle.Components
{
    public struct FightCameraComponent
    {
        public float maxTimeToStopFollowingInPlayer;
        public float maxTimeToLerpInPlayer;
    }

    public struct NeedLerpPositionCameraFollowingToTargetComponent
    {
        public float currentTime;
        public float maxTime;
    }
    public struct TargetCameraComponent
    {
        public Transform positionThirdTarget;
        public Transform positionPlayerTransform;
    }
}