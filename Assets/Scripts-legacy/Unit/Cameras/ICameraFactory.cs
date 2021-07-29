using Interface;
using UnityEngine;


namespace Unit.Cameras
{
    public interface ICameraFactory
    {
        IFightCamera CreateCamera(Camera baseCamera);
    }
}