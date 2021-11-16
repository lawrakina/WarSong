using Code.Data;
using Code.Extension;
using Guirao.UltimateTextDamage;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Rendering;


namespace Code.GameCamera
{
    public sealed class FightCamera : MonoBehaviour
    {
        [Header("Third person camera orientation")]
        [SerializeField]
        private Vector3 _offsetThirdPosition = new Vector3(0.0f, 10.0f, -24.0f);

        [SerializeField]
        private Vector3 _offsetThirdRotation = new Vector3(0.0f, 0.0f, 0.0f);
        
        #region Properties

        public Transform ThirdTarget { get; set; }
        public EcsEntity Entity { get; set; }

        public Transform Transform => gameObject.transform;
        public CameraSettings Settings { get; set; }

        public Camera Camera { get; set; }
        public Vector3 OffsetThirdPosition => _offsetThirdPosition;

        public UltimateTextDamageManager UiTextManager { get; set; }

        #endregion


        private void OnCollisionEnter(Collision other)
        {
            Dbg.Log($"OnTriggerEnter.other:{other},{other.gameObject},isStatic:{other.gameObject.isStatic}");
            if (other.gameObject.isStatic)
            {
                var go = other.gameObject.GetComponent<MeshRenderer>();
                go.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            Dbg.Log($"OnTriggerExit.other:{other},{other.gameObject},isStatic:{other.gameObject.isStatic}");
            if (other.gameObject.isStatic)
            {
                var go = other.gameObject.GetComponent<MeshRenderer>();
                go.shadowCastingMode = ShadowCastingMode.On;
            }
        }
    }
}