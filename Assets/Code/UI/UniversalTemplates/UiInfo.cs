using System;
using UnityEngine;


namespace Code.UI.UniversalTemplates
{
    [Serializable]
    public class UiInfo
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;
        [SerializeField] private string _description;

        public Sprite Icon => _icon;
        public string Title => _title;
        public string Description => _description;

        public void Init(string title, string description, Sprite icon){
            _title = title;
            _description = description;
            _icon = icon;
        }
    }
}