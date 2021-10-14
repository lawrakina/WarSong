using System;
using UnityEngine;


namespace Code.Data.Abilities{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(UiInfo), menuName = "Abilities/" + nameof(UiInfo))]
    public class UiInfo : ScriptableObject{
        [SerializeField]
        private string _title;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Sprite _icon;

        public string Title => _title;
        public string Description => _description;
        public Sprite Icon => _icon;
    }
}