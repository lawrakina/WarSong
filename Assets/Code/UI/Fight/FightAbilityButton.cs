using System;
using Code.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Fight{
    public class FightAbilityButton : MonoBehaviour, IPointerDownHandler{
        [SerializeField]
        private Image _cooldown;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private AbilityCellType _cellType;
        private Ability _ability;
        private Action<Ability> _actionOfAbility;
        public AbilityCellType AbilityCellType => _cellType;

        public void Init(Ability ability, Action<Ability> actionOfAbility){
            _ability = ability;
            _actionOfAbility = actionOfAbility;
            _icon.sprite = _ability.Cell.Body.uiInfo.Icon;
        }

        public void OnPointerDown(PointerEventData eventData){
            _actionOfAbility?.Invoke(_ability);
        }

        public void Recharge(float value){
            if (value >= 1.0f){
                _cooldown.gameObject.SetActive(false);
            } else{
                _cooldown.gameObject.SetActive(true);
                _cooldown.fillAmount =1 - value;
            }
        }
    }
}