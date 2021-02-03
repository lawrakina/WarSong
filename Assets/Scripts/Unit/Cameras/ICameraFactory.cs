using UnityEngine;


namespace Unit.Cameras
{
    public interface ICameraFactory
    {
        FightCamera CreateCamera(Camera baseCamera);
    }
}