using Data;
using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using UnityEngine;


namespace Interface
{
    public interface IFightCamera
    {
        Transform ThirdTarget { get; set; }
        EcsEntity Entity { get; set; }
        Transform Transform { get; }
        CameraSettingsInBattle Settings { get; set; }
        UltimateTextDamageManager UiTextManager { get; set; }
        Vector3 OffsetTopPosition();
        Vector3 OffsetThirdPosition();
    }
}