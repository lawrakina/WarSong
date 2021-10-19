using Code.Data;
using Code.Data.Abilities;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character.Abilities{
    public sealed class CellAbilityClickHandler : MonoBehaviour, IPointerDownHandler{
        [SerializeField]
        private Image _image;
        private ReactiveCommand<AbilityCell> _command;
        private AbilityCell _ability;
        public AbilityCellType TargetTypesOfAbility;

        public void Init(AbilityCell ability, ReactiveCommand<AbilityCell> command){
            _ability = ability;
            _command = command;
            if (!_ability.IsEmpty){
                _image.enabled = true;
                _image.sprite = _ability.Body.uiInfo.Icon;
            }
            else{
                _image.enabled = false;
            }
        }

        public void Clear(){
            _image.sprite = null;
            _image.enabled = false;
            _ability = null;
            _command = null;
        }

        public void OnPointerDown(PointerEventData eventData){
            _command?.Execute(_ability);
        }
    }
}