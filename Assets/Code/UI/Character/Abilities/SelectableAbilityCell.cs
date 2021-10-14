using System;
using Code.Data;
using Code.Data.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Code.UI.Character.Abilities{
    public sealed class SelectableAbilityCell : MonoBehaviour, IPointerDownHandler{
        private bool _isSelect;
        [SerializeField]
        private GameObject _selectableBackGround;
        private AbilityCell _body;
        [SerializeField]
        private Image _bodyImage;
        [SerializeField]
        private Text _title;
        public Action<SelectableAbilityCell> Command{ get; set; }
        public AbilityCellType CellType{ get; set; }
        public bool IsSelect{
            get => _isSelect;
            set{
                _isSelect = value;
                _selectableBackGround.SetActive(_isSelect);
            }
        }
        public AbilityCell Body{
            get => _body;
            private set{
                if (value == null){
                    _bodyImage.enabled = false;
                }
                else{
                    _bodyImage.enabled = true;
                    _bodyImage.sprite = value.Body.uiInfo.Icon;
                }

                _body = value;
            }
        }

        public void OnPointerDown(PointerEventData eventData){
            Command?.Invoke(this);
        }

        public void Init(AbilityCell body, AbilityCellType abilityCellType, Action<SelectableAbilityCell> action){
            this.name = body.ToString();
            Command = action;
            CellType = abilityCellType;
            Body = body;
            if (CellType != AbilityCellType.IsStock){
                _title.text = body.Body.uiInfo.Title;
            }
        }
    }
}