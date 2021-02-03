using UnityEngine;


namespace Interface
{
    public interface IFightCamera
    {
        Transform TopTarget { get; set; }
        Transform ThirdTarget { get; set; }
        Vector3 OffsetTopPosition();
        Vector3 OffsetThirdPosition();
    }
}