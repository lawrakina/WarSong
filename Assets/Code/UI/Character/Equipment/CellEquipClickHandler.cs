using Code.Data;
using Code.Data.Unit;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character.Equipment{
    public sealed class CellEquipClickHandler : MonoBehaviour, IPointerDownHandler{
        [SerializeField]
        private Image _image;
        private ReactiveCommand<EquipCell> _command;
        public TargetEquipCell[] TargetTypesOfEquip;
        private EquipCell _equip;
        private bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;

        public void Init(EquipCell equip, ReactiveCommand<EquipCell> command){
            _equip = equip;
            _command = command;
            _isInitialized = true;
            if (!_equip.IsEmpty){
                _image.enabled = true;
                _image.sprite = _equip.Body.UiInfo.Icon;
            } else{
                _image.enabled = false;
            }
        }

        public void Clear(){
            _image.sprite = null;
            _image.enabled = false;
            _equip = null;
            _command = null;
            _isInitialized = false;
        }

        public void OnPointerDown(PointerEventData eventData){
            _command?.Execute(_equip);
        }
    }
}