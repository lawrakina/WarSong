using System.Collections.Generic;
using Code.Extension;
using Code.UI.DragAndDrop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Code.UI.Character.Abilities{
    public sealed class CharEditReplacementAbilitiesView : UiWindow, IHasChanged{
        [SerializeField]
        private Button _close;
        [SerializeField]
        private Transform _selectedAbilities;
        [SerializeField]
        private Transform _listOfAbilities;
        [SerializeField]
        private Text _titleSelectedAbility;
        [SerializeField]
        private Text _descriptionSelectedAbility;
        [SerializeField]
        private Image _icoSelectedAbility;
        [SerializeField]
        private Button _detailFromSelectedAbility;
        [SerializeField]
        private Button _improveSelectedAbility;
        private UnityAction<Transform, Transform> _hasChanged;
        public string TitleSelectedAbility{
            set => _titleSelectedAbility.text = value;
        }
        public string DescriptionSelectedAbility{
            set => _descriptionSelectedAbility.text = value;
        }
        public Sprite IconSelectedAbility{
            set => _icoSelectedAbility.sprite = value;
        }
        public Button ButtonImpove => _improveSelectedAbility;
        public Transform SelectedAbilities => _selectedAbilities;
        public Transform ListOfAbilities => _listOfAbilities;

        public void Init(UnityAction detailFromSelectedAbility, UnityAction improveSelectedAbility,
            UnityAction closeView, UnityAction<Transform, Transform> hasChanged){
            _detailFromSelectedAbility.onClick.AddListener(detailFromSelectedAbility);
            _improveSelectedAbility.onClick.AddListener(improveSelectedAbility);
            _close.onClick.AddListener(closeView);
            _hasChanged = hasChanged;
        }

        ~CharEditReplacementAbilitiesView(){
            _detailFromSelectedAbility.onClick.RemoveAllListeners();
            _improveSelectedAbility.onClick.RemoveAllListeners();
            _close.onClick.RemoveAllListeners();
        }

        public void Clear(){
            var children = new List<GameObject>();
            foreach (Transform child in _selectedAbilities) children.Add(child.gameObject);
            foreach (Transform child in _listOfAbilities) children.Add(child.gameObject);
            children.ForEach(Destroy);
        }

        public void HasChanged(Transform sender, Transform dropped){
            _hasChanged?.Invoke(sender, dropped);
        }
    }
}