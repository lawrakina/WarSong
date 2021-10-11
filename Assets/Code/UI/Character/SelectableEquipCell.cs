using System;
using Code.Data;
using Code.Equipment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Character
{
    public sealed class SelectableEquipCell : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _selectableBackGround;
        [SerializeField] private GameObject _equippedItemFlag;
        [SerializeField] private Image _bodyImage;
        private bool _isSelect;
        private BaseEquipItem _body;

        public Action<SelectableEquipCell> Command { set; get; }
        public EquipCellType CellType { get; set; }


        public BaseEquipItem Body
        {
            get => _body;
            private set
            {
                if (value == null)
                {
                    _bodyImage.enabled = false;
                }
                else
                {
                    _bodyImage.enabled = true;
                    _bodyImage.sprite = value.UiInfo.Icon;
                }
                _body = value;
            }
        }

        public bool IsSelect
        {
            get
            {
                return _isSelect;
            }
            set
            {
                _isSelect = value;
                _selectableBackGround.SetActive(_isSelect);
            }
        }

        public void Init(BaseEquipItem body, EquipCellType cellType, Action<SelectableEquipCell> onSelectItem, bool equipped = false)
        {
            this.name = body.ToString();
            Command = onSelectItem;
            CellType = cellType;
            Body = body;
            _equippedItemFlag.SetActive(equipped);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Command.Invoke(this);
        }
    }
}