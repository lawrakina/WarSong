using Extension;
using UnityEngine;


namespace Unit.Cameras
{
    public sealed class CameraFactory : ICameraFactory
    {
        public FightCamera CreateCamera(Camera baseCamera)
        {
            var component = baseCamera.gameObject.AddCode<FightCamera>();
            return component.GetComponent<FightCamera>();
        }
    }
}