﻿using System;
using Code.Data;
using Code.Extension;
using DG.Tweening;
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
        private Image _selected;
        [SerializeField]
        private AbilityCellType _cellType;
        private Ability _ability;
        private Action<Ability> _actionOfAbility;
        public AbilityCellType AbilityCellType => _cellType;

        public void Init(Ability ability, Action<Ability> actionOfAbility){
            _ability = ability;
            _actionOfAbility = actionOfAbility;
            _icon.sprite = _ability.Cell.Body.uiInfo.Icon;
            _selected.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData){
            Dbg.Log($"OnPointerDown.{this}");
            _actionOfAbility?.Invoke(_ability);
        }

        public void Recharge(float value){
            if (value >= 1.0f){
                _cooldown.gameObject.SetActive(false);
                _icon.transform.DOShakeScale(
                    GlobalUiAnimationSettings.ABILITY_BUTTON_SHAKE_DURATION,
                    GlobalUiAnimationSettings.ABILITY_BUTTON_SHAKE_STRENGTH,
                    GlobalUiAnimationSettings.ABILITY_BUTTON_SHAKE_VIBRATO);
            } else{
                _cooldown.gameObject.SetActive(true);
                _cooldown.fillAmount = 1 - value;
            }
        }

        public void Selected(bool state){
            Dbg.Log($"Change state({state}) of ability button:{this}");
            _selected.gameObject.SetActive(state);
        }
    }
}