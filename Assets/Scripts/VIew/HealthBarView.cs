using Interface;
using UnityEngine;


namespace VIew
{
    public class HealthBarView : MonoBehaviour, IEnabled
    {
        public void AlignCamera(Transform cameraTransform)
        {
            var camXform = cameraTransform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }

        public void UpdateParams(float hp, float maxHp)
        {
            MeshRenderer.GetPropertyBlock(MatBlock);
            MatBlock.SetFloat(Fill, hp / maxHp);
            MeshRenderer.SetPropertyBlock(MatBlock);
        }


        #region Fields

        [HideInInspector]
        public MaterialPropertyBlock MatBlock;

        [HideInInspector]
        public MeshRenderer MeshRenderer;

        private static readonly int Fill = Shader.PropertyToID("_Fill");

        #endregion


        #region UnityMethods

        public void On()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
            MatBlock = new MaterialPropertyBlock();
        }

        public void Off()
        {
        }

        #endregion
    }
}