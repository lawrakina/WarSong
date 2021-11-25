using Code.Fight.EcsBattle.Unit.EnemyHealthBars;
using TMPro;
using UnityEngine;


namespace Code.Unit
{
    public class HealthBarView : MonoBehaviour
    {
        #region fields

        private MaterialPropertyBlock _matBlock;
        private MeshRenderer _meshRenderer;
        private Transform _camera;
        private TMP_Text _enemyLvl;
        private TMP_Text _enemyName;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _matBlock = new MaterialPropertyBlock();
            _enemyName = GetComponentInChildren<EnemyNameUiView>().enemyName;
            _enemyLvl = GetComponentInChildren<EnemyLvlUiView>().enemyLvl;
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
        
        //TODO включение/выключение объекта

        public void SetOnOff(bool value)
        {
            gameObject.SetActive(value);
        }

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

        public void SetEnemyName(string name)
        {
            _enemyName.text = name;
        }

        public void SetEnemyLvl(int lvl)
        {
            _enemyLvl.text = lvl.ToString();
        }
        
        public void SetActive(bool state){
                _meshRenderer.enabled = state;
        }

        #endregion
    }
}