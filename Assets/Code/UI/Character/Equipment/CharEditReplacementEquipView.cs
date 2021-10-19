using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.UI.Character.Equipment{
    public sealed class CharEditReplacementEquipView : UiWindow{
        [SerializeField]
        private Button _close;
        [SerializeField]
        private Button _equipUnequipButton;
        [SerializeField]
        private GameObject _grid;
        [SerializeField]
        private Text _selectTitleItem;
        [SerializeField]
        private Text _selectDescriptionItem;
        [SerializeField]
        private Text _summaryItemLevel;
        [SerializeField]
        private Text _summaryInfoByChar;

        public Transform Grid => _grid.transform;

        public string InfoTitleSelectedItem{
            set => _selectTitleItem.text = value;
        }

        public string InfoDescriptionSelectedItem{
            set => _selectDescriptionItem.text = value;
        }

        public string InfoItemLevel{
            set => _summaryItemLevel.text = value;
        }

        public string InfoSummaryByChar{
            set => _summaryInfoByChar.text = value;
        }

        public void Init(UnityAction putOnOrTakeoffItem, UnityAction closeView){
            _equipUnequipButton.onClick.AddListener(putOnOrTakeoffItem);
            _close.onClick.AddListener(closeView);
        }

        public void Clear(){
            var children = new List<GameObject>();
            foreach (Transform child in _grid.transform) children.Add(child.gameObject);
            children.ForEach(Destroy);

        }

        ~CharEditReplacementEquipView(){
            _close.onClick.RemoveAllListeners();
            _equipUnequipButton.onClick.RemoveAllListeners();
        }
    }
}