using Code.Data;
using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using UnityEngine;


namespace Code.GameCamera
{
    public interface IFightCamera
    {
        Transform ThirdTarget { get; set; }
        EcsEntity Entity { get; set; }
        Transform Transform { get; }
        CameraSettings Settings { get; set; }
        UltimateTextDamageManager UiTextManager { get; set; }
        Camera Camera { get; set; }
        Vector3 OffsetThirdPosition { get; }
    }
}