using UnityEngine;


namespace VIew
{
    public class HealthBarView : MonoBehaviour
    {
        #region fields

        private MaterialPropertyBlock _matBlock;
        private MeshRenderer _meshRenderer;
        private Transform _camera;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _matBlock = new MaterialPropertyBlock();
        }

        #endregion


        //todo сделать проверку нажив\мертв и изменить вид Bar`a 
        // private void Update() {
        //     // Only display on partial health
        //     if (damageable.CurrentHealth < damageable.MaxHealth) {
        //         meshRenderer.enabled = true;
        //         AlignCamera();
        //     } else {
        //         meshRenderer.enabled = false;
        //     }
        // }


        #region Methods

        public void SetCamera(Transform camera)
        {
            _camera = camera;
        }

        public void AlignCamera()
        {
            var camXform = _camera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }

        public void ChangeValue(float currentValue, float maxValue)
        {
            _meshRenderer.GetPropertyBlock(_matBlock);
            _matBlock.SetFloat("_Fill", currentValue / maxValue);
            _meshRenderer.SetPropertyBlock(_matBlock);
        }

        #endregion
    }
}