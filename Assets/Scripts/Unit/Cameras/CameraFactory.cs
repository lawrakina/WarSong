using Extension;
using UnityEngine;


namespace Unit.Cameras
{
    public sealed class CameraFactory : ICameraFactory
    {
        public FightCamera CreateCamera(Camera baseCamera)
        {
            var component = baseCamera.gameObject.AddCode<FightCamera>();
                /*.AddCapsuleCollider(
                1.87f,
                false,
                new Vector3(0.0f,2.0f,13.0f), 
                24.0f, 
                2);*/
            return component.GetComponent<FightCamera>();
        }
    }
}