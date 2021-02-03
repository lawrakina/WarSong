using UnityEngine;


public sealed class MyCameraView : MonoBehaviour
{
    [SerializeField]
    public float _cameraSpeed = 5.0f;

    [SerializeField]
    public Vector3 _offSetPosition = new Vector3(0.0f, 10.0f, -24.0f);

    [SerializeField]
    public Vector3 _offSetRotation = new Vector3(33.0f, 0.0f, 0.0f);
}